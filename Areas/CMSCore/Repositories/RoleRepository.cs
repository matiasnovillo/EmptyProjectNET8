using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;
using EmptyProject.Areas.CMSCore.DTOs;
using EmptyProject.Areas.CMSCore.Interfaces;
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

namespace EmptyProject.Areas.CMSCore.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly EFCoreContext _context;

        public RoleRepository(EFCoreContext context)
        {
            _context = context;
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
                return _context.Role
                                .FirstOrDefault(x => x.RoleId == roleId);
            }
            catch (Exception) { throw; }
        }

        public List<Role> GetAll()
        {
            try
            {
                return _context.Role.ToList();
            }
            catch (Exception) { throw; }
        }

        public paginatedRoleDTO GetAllByRoleIdPaginated(string textToSearch,
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

                var paginatedRole = _context.Role
                        .Where(x => strictSearch ?
                            words.All(word => x.RoleId.ToString().Contains(word)) :
                            words.Any(word => x.RoleId.ToString().Contains(word)))
                        .OrderBy(p => p.RoleId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                return new paginatedRoleDTO
                {
                    lstRole = paginatedRole,
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
                _context.Role.Add(role);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Role role)
        {
            try
            {
                _context.Role.Update(role);
                return _context.SaveChanges() > 0;
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

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Other methods
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
