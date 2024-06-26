using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EmptyProject.Areas.CMS.UserBack.Entities;
using EmptyProject.Areas.CMS.UserBack.DTOs;
using EmptyProject.Areas.CMS.UserBack.Interfaces;
using EmptyProject.DatabaseContexts;
using System.Text.RegularExpressions;
using System.Data;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace EmptyProject.Areas.CMS.UserBack.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly EmptyProjectContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        public UserRepository(EmptyProjectContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
        }

        public IQueryable<User> AsQueryable()
        {
            try
            {
                return _context.User.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.User.Count();
            }
            catch (Exception) { throw; }
        }

        public User? GetByUserId(int userId)
        {
            try
            {
                //Look in cache first
                if (!_memoryCache.TryGetValue($@"CMS.User.UserId={userId}", out User? user))
                {
                    //If not exist in cache, look in DB
                    user = _context.User
                                .FirstOrDefault(x => x.UserId == userId);
                    
                    if (user != null)
                    {
                        _memoryCache.Set(userId, user, _memoryCacheEntryOptions);
                    }
                }
                return user;
            }
            catch (Exception) { throw; }
        }

        public List<User?> GetAll()
        {
            try
            {
                return _context.User.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<User> GetAllByUserIdForModal(string textToSearch)
        {
            try
            {
                var query = from user in _context.User
                            select new { User = user };

                // Extraemos los resultados en listas separadas
                List<User> lstUser = query.Select(result => result.User)
                        .Where(x => x.UserId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstUser;
            }
            catch (Exception) { throw; }
        }

        public List<User?> GetAllByUserId(List<int> lstUserChecked)
        {
            try
            {
                List<User?> lstUser = [];

                foreach (int UserId in lstUserChecked)
                {
                    User user = _context.User.Where(x => x.UserId == UserId).FirstOrDefault();

                    if (user != null)
                    {
                        lstUser.Add(user);
                    }
                }

                return lstUser;
            }
            catch (Exception) { throw; }
        }

        public paginatedUserDTO GetAllByEmailPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex, 
            int pageSize)
        {
            try
            {
                //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
                string[] words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                int TotalUser = _context.User.Count();

                List<User> lstUser = _context.User
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.Email.Contains(word)) :
                            words.Any(word => x.Email.Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (User user in lstUser)
                {

                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == user.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == user.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedUserDTO
                {
                    lstUser = lstUser,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalUser,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }

        public List<User?> GetAllByEmail(string textToSearch,
    bool strictSearch)
        {
            //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
            string[] words = Regex
                .Replace(textToSearch
                .Trim(), @"\s+", " ")
                .Split(" ");

            List<User?> lstUser = [];

            var GetAllQuery = AsQueryable()
                .Where(x => strictSearch ?
                    words.All(word => x.Email.Contains(word)) :
                    words.Any(word => x.Email.Contains(word)))
                .ToList();

            foreach (var x in GetAllQuery)
            {
                User user = new()
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    Password = x.Password
                };
                lstUser.Add(user);
            }

            return lstUser;
        }

        public User? GetByEmailAndPassword(string email,
            string password)
        {
            return _context.User.AsQueryable()
                .Where(u => u.Password == password)
                .Where(u => u.Email == email)
                .FirstOrDefault();
        }
        #endregion

        #region Non-Queries
        public bool Add(User user)
        {
            try
            {
                EntityEntry<User> UserToAdd = _context.User.Add(user);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    int AddedUserId = UserToAdd.Entity.UserId;

                    _memoryCache.Set($@"CMS.User.UserId={AddedUserId}", user, _memoryCacheEntryOptions);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public bool Update(User user)
        {
            try
            {
                _context.User.Update(user);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Set($@"CMS.User.UserId={user.UserId}", user, _memoryCacheEntryOptions);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByUserId(int userId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.UserId == userId)
                        .ExecuteDelete();

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Remove($@"CMS.User.UserId={userId}");
                }

                return result;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByUserIdInDataTable(List<int> lstUserChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("UserId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("RoleId", typeof(string));
                DataTable.Columns.Add("ProfilePicture", typeof(string));
                

                foreach (int UserId in lstUserChecked)
                {
                    User user = _context.User.Where(x => x.UserId == UserId).FirstOrDefault();

                    if (user != null)
                    {
                        DataTable.Rows.Add(
                        user.UserId,
                        user.Active,
                        user.DateTimeCreation,
                        user.DateTimeLastModification,
                        user.UserCreationId,
                        user.UserLastModificationId,
                        user.Email,
                        user.Password,
                        user.RoleId,
                        user.ProfilePicture
                        );
                    }
                }                

                return DataTable;
            }
            catch (Exception) { throw; }
        }

        public DataTable GetAllInDataTable()
        {
            try
            {
                List<User> lstUser = _context.User.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("UserId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("RoleId", typeof(string));
                DataTable.Columns.Add("ProfilePicture", typeof(string));
                

                foreach (User user in lstUser)
                {
                    DataTable.Rows.Add(
                        user.UserId,
                        user.Active,
                        user.DateTimeCreation,
                        user.DateTimeLastModification,
                        user.UserCreationId,
                        user.UserLastModificationId,
                        user.Email,
                        user.Password,
                        user.RoleId,
                        user.ProfilePicture
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
