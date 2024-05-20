using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.DTOs;
using EmptyProject.Areas.CMSCore.Interfaces;
using System.Data;
using EmptyProject.DatabaseContexts;

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

namespace EmptyProject.Areas.CMSCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly EmptyProjectContext _context;

        public UserRepository(EmptyProjectContext context)
        {
            _context = context;
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
        public  int Count()
        {
            try
            {
                return _context.User.Count();
            }
            catch (Exception) { throw; }
        }

        public  User? GetByUserId(int userId)
        {
            try
            {
                return  _context.User
                                .FirstOrDefault(x => x.UserId == userId);
            }
            catch (Exception) { throw; }
        }

        public  List<User> GetAll()
        {
            try
            {
                return  _context.User.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<User> GetAllByRoleAsRoot()
        {
            try
            {
                return _context.User
                    .Where(x => x.RoleId == 1) //Only Root
                    .ToList();
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

                int TotalUser =  _context.User.Count();

                var query = from user in _context.User
                            join userCreation in _context.User on user.UserCreationId equals userCreation.UserId
                            join userLastModification in _context.User on user.UserLastModificationId equals userLastModification.UserId
                            join role in _context.Role on user.RoleId equals role.RoleId
                            select new { User = user, UserCreation = userCreation, UserLastModification = userLastModification, Role = role };

                // Extraemos los resultados en listas separadas
                List<User> lstUser = query.Select(result => result.User)
                        .Where(x => strictSearch ?
                            words.All(word => x.Email.Contains(word)) :
                            words.Any(word => x.Email.Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = query.Select(result => result.UserCreation).ToList();
                List<User> lstUserLastModification = query.Select(result => result.UserLastModification).ToList();
                List<Role> lstRole = query.Select(result => result.Role).ToList();

                return new paginatedUserDTO
                {
                    lstUser = lstUser,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    lstRole = lstRole,
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
            return  _context.User.AsQueryable()
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
                 _context.User.Add(user);
                return  _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(User user)
        {
            try
            {
                _context.User.Update(user);
                return  _context.SaveChanges() > 0;
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

                return  _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Other methods
        public DataTable GetAllInDataTable()
        {
            try
            {
                List<User> lstUser =  _context.User.ToList();

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
                        user.RoleId
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
