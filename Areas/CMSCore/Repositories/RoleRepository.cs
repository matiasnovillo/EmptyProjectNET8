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
    public class RoleRepository : IRoleRepository
    {
        protected readonly EmptyProjectContext _context;

        public RoleRepository(EmptyProjectContext context)
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

                var query = from role in _context.Role
                            join userCreation in _context.User on role.UserCreationId equals userCreation.UserId
                            join userLastModification in _context.User on role.UserLastModificationId equals userLastModification.UserId
                            select new { Role = role, UserCreation = userCreation, UserLastModification = userLastModification };

                // Extraemos los resultados en listas separadas
                List<Role> lstRole = query.Select(result => result.Role)
                        .Where(x => strictSearch ?
                            words.All(word => x.Name.Contains(word)) :
                            words.Any(word => x.Name.Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = query.Select(result => result.UserCreation).ToList();
                List<User> lstUserLastModification = query.Select(result => result.UserLastModification).ToList();

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
