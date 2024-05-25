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
    public class UserConfigurationUIRepository : IUserConfigurationUIRepository
    {
        protected readonly EmptyProjectContext _context;

        public UserConfigurationUIRepository(EmptyProjectContext context)
        {
            _context = context;
        }

        public IQueryable<UserConfigurationUI> AsQueryable()
        {
            try
            {
                return _context.UserConfigurationUI.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.UserConfigurationUI.Count();
            }
            catch (Exception) { throw; }
        }

        public UserConfigurationUI? GetByUserConfigurationUIId(int userconfigurationuiId)
        {
            try
            {
                return _context.UserConfigurationUI
                            .FirstOrDefault(x => x.UserConfigurationUIId == userconfigurationuiId);
            }
            catch (Exception) { throw; }
        }

        public UserConfigurationUI? GetByName(string name)
        {
            try
            {
                return _context.UserConfigurationUI
                            .FirstOrDefault(x => x.Name == name);
            }
            catch (Exception) { throw; }
        }

        public List<UserConfigurationUI?> GetAll()
        {
            try
            {
                return _context.UserConfigurationUI.ToList();
            }
            catch (Exception) { throw; }
        }

        public paginatedUserConfigurationUIDTO GetAllByUserConfigurationUIIdPaginated(string textToSearch,
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

                int TotalUserConfigurationUI = _context.UserConfigurationUI.Count();

                var query = from userconfigurationui in _context.UserConfigurationUI
                            join userCreation in _context.User on userconfigurationui.UserCreationId equals userCreation.UserId
                            join userLastModification in _context.User on userconfigurationui.UserLastModificationId equals userLastModification.UserId
                            select new { UserConfigurationUI = userconfigurationui, UserCreation = userCreation, UserLastModification = userLastModification };

                // Extraemos los resultados en listas separadas
                List<UserConfigurationUI> lstUserConfigurationUI = query.Select(result => result.UserConfigurationUI)
                        .Where(x => strictSearch ?
                            words.All(word => x.UserConfigurationUIId.ToString().Contains(word)) :
                            words.Any(word => x.UserConfigurationUIId.ToString().Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = query.Select(result => result.UserCreation).ToList();
                List<User> lstUserLastModification = query.Select(result => result.UserLastModification).ToList();

                return new paginatedUserConfigurationUIDTO
                {
                    lstUserConfigurationUI = lstUserConfigurationUI,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalUserConfigurationUI,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(UserConfigurationUI userconfigurationui)
        {
            try
            {
                _context.UserConfigurationUI.Add(userconfigurationui);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(UserConfigurationUI userconfigurationui)
        {
            try
            {
                _context.UserConfigurationUI.Update(userconfigurationui);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByUserConfigurationUIId(int userconfigurationuiId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.UserConfigurationUIId == userconfigurationuiId)
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
                List<UserConfigurationUI> lstUserConfigurationUI = _context.UserConfigurationUI.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("UserConfigurationUIId", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("ValueAsText", typeof(string));
                DataTable.Columns.Add("UserId", typeof(string));
                

                foreach (UserConfigurationUI userconfigurationui in lstUserConfigurationUI)
                {
                    DataTable.Rows.Add(
                        userconfigurationui.UserConfigurationUIId,
                        userconfigurationui.DateTimeCreation,
                        userconfigurationui.DateTimeLastModification,
                        userconfigurationui.UserCreationId,
                        userconfigurationui.UserLastModificationId,
                        userconfigurationui.Name,
                        userconfigurationui.ValueAsText,
                        userconfigurationui.UserId
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}