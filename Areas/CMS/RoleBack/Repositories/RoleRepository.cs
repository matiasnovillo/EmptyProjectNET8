using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EmptyProject.Areas.CMS.UserBack.Entities;
using EmptyProject.Areas.CMS.RoleBack.Entities;
using EmptyProject.Areas.CMS.RoleBack.DTOs;
using EmptyProject.Areas.CMS.RoleBack.Interfaces;
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

namespace EmptyProject.Areas.CMS.RoleBack.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly EmptyProjectContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        public RoleRepository(EmptyProjectContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
        }

        public IQueryable<Role> AsQueryable()
        {
            try
            {
                return _context.Role.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Role.Count();
            }
            catch (Exception) { throw; }
        }

        public Role? GetByRoleId(int roleId)
        {
            try
            {
                //Look in cache first
                if (!_memoryCache.TryGetValue($@"CMS.Role.RoleId={roleId}", out Role? role))
                {
                    //If not exist in cache, look in DB
                    role = _context.Role
                                .FirstOrDefault(x => x.RoleId == roleId);
                    
                    if (role != null)
                    {
                        _memoryCache.Set(roleId, role, _memoryCacheEntryOptions);
                    }
                }
                return role;
            }
            catch (Exception) { throw; }
        }

        public List<Role?> GetAll()
        {
            try
            {
                return _context.Role.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<Role> GetAllByNameForModal(string textToSearch)
        {
            try
            {
                var query = from role in _context.Role
                            select new { Role = role };

                // Extraemos los resultados en listas separadas
                List<Role> lstRole = query.Select(result => result.Role)
                        .Where(x => x.Name.Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstRole;
            }
            catch (Exception) { throw; }
        }

        public List<Role?> GetAllByRoleId(List<int> lstRoleChecked)
        {
            try
            {
                List<Role?> lstRole = [];

                foreach (int RoleId in lstRoleChecked)
                {
                    Role role = _context.Role.Where(x => x.RoleId == RoleId).FirstOrDefault();

                    if (role != null)
                    {
                        lstRole.Add(role);
                    }
                }

                return lstRole;
            }
            catch (Exception) { throw; }
        }

        public paginatedRoleDTO GetAllByNamePaginated(string textToSearch,
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

                int TotalRole = _context.Role.Count();

                List<Role> lstRole = _context.Role
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.Name.Contains(word)) :
                            words.Any(word => x.Name.Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (Role role in lstRole)
                {

                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == role.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == role.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedRoleDTO
                {
                    lstRole = lstRole,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalRole,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(Role role)
        {
            try
            {
                EntityEntry<Role> RoleToAdd = _context.Role.Add(role);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    int AddedRoleId = RoleToAdd.Entity.RoleId;

                    _memoryCache.Set($@"CMS.Role.RoleId={AddedRoleId}", role, _memoryCacheEntryOptions);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Role role)
        {
            try
            {
                _context.Role.Update(role);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Set($@"CMS.Role.RoleId={role.RoleId}", role, _memoryCacheEntryOptions);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByRoleId(int roleId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.RoleId == roleId)
                        .ExecuteDelete();

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Remove($@"CMS.Role.RoleId={roleId}");
                }

                return result;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByRoleIdInDataTable(List<int> lstRoleChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("RoleId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (int RoleId in lstRoleChecked)
                {
                    Role role = _context.Role.Where(x => x.RoleId == RoleId).FirstOrDefault();

                    if (role != null)
                    {
                        DataTable.Rows.Add(
                        role.RoleId,
                        role.Active,
                        role.DateTimeCreation,
                        role.DateTimeLastModification,
                        role.UserCreationId,
                        role.UserLastModificationId,
                        role.Name
                        
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
                List<Role> lstRole = _context.Role.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("RoleId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (Role role in lstRole)
                {
                    DataTable.Rows.Add(
                        role.RoleId,
                        role.Active,
                        role.DateTimeCreation,
                        role.DateTimeLastModification,
                        role.UserCreationId,
                        role.UserLastModificationId,
                        role.Name
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
