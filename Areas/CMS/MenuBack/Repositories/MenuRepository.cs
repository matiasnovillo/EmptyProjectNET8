using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EmptyProject.Areas.CMS.UserBack.Entities;
using EmptyProject.Areas.CMS.MenuBack.Entities;
using EmptyProject.Areas.CMS.MenuBack.DTOs;
using EmptyProject.Areas.CMS.MenuBack.Interfaces;
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

namespace EmptyProject.Areas.CMS.MenuBack.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        protected readonly EmptyProjectContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        public MenuRepository(EmptyProjectContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
        }

        public IQueryable<Menu> AsQueryable()
        {
            try
            {
                return _context.Menu.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Menu.Count();
            }
            catch (Exception) { throw; }
        }

        public Menu? GetByMenuId(int menuId)
        {
            try
            {
                //Look in cache first
                if (!_memoryCache.TryGetValue($@"CMS.Menu.MenuId={menuId}", out Menu? menu))
                {
                    //If not exist in cache, look in DB
                    menu = _context.Menu
                                .FirstOrDefault(x => x.MenuId == menuId);
                    
                    if (menu != null)
                    {
                        _memoryCache.Set(menuId, menu, _memoryCacheEntryOptions);
                    }
                }
                return menu;
            }
            catch (Exception) { throw; }
        }

        public List<Menu?> GetAll()
        {
            try
            {
                return _context.Menu.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<Menu> GetAllByMenuIdForModal(string textToSearch)
        {
            try
            {
                var query = from menu in _context.Menu
                            select new { Menu = menu };

                // Extraemos los resultados en listas separadas
                List<Menu> lstMenu = query.Select(result => result.Menu)
                        .Where(x => x.MenuId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstMenu;
            }
            catch (Exception) { throw; }
        }

        public List<Menu?> GetAllByMenuId(List<int> lstMenuChecked)
        {
            try
            {
                List<Menu?> lstMenu = [];

                foreach (int MenuId in lstMenuChecked)
                {
                    Menu menu = _context.Menu.Where(x => x.MenuId == MenuId).FirstOrDefault();

                    if (menu != null)
                    {
                        lstMenu.Add(menu);
                    }
                }

                return lstMenu;
            }
            catch (Exception) { throw; }
        }

        public paginatedMenuDTO GetAllByNamePaginated(string textToSearch,
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

                int TotalMenu = _context.Menu.Count();

                List<Menu> lstMenu = _context.Menu
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

                foreach (Menu menu in lstMenu)
                {
                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == menu.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == menu.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedMenuDTO
                {
                    lstMenu = lstMenu,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalMenu,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(Menu menu)
        {
            try
            {
                EntityEntry<Menu> MenuToAdd = _context.Menu.Add(menu);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    int AddedMenuId = MenuToAdd.Entity.MenuId;

                    _memoryCache.Set($@"CMS.Menu.MenuId={AddedMenuId}", menu, _memoryCacheEntryOptions);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Menu menu)
        {
            try
            {
                _context.Menu.Update(menu);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Set($@"CMS.Menu.MenuId={menu.MenuId}", menu, _memoryCacheEntryOptions);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByMenuId(int menuId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.MenuId == menuId)
                        .ExecuteDelete();

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Remove($@"CMS.Menu.MenuId={menuId}");
                }

                return result;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByMenuIdInDataTable(List<int> lstMenuChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("MenuId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("MenuFatherId", typeof(string));
                DataTable.Columns.Add("Order", typeof(string));
                DataTable.Columns.Add("URLPath", typeof(string));
                DataTable.Columns.Add("IconURLPath", typeof(string));
                

                foreach (int MenuId in lstMenuChecked)
                {
                    Menu menu = _context.Menu.Where(x => x.MenuId == MenuId).FirstOrDefault();

                    if (menu != null)
                    {
                        DataTable.Rows.Add(
                        menu.MenuId,
                        menu.Active,
                        menu.DateTimeCreation,
                        menu.DateTimeLastModification,
                        menu.UserCreationId,
                        menu.UserLastModificationId,
                        menu.Name,
                        menu.MenuFatherId,
                        menu.Order,
                        menu.URLPath,
                        menu.IconURLPath
                        
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
                List<Menu> lstMenu = _context.Menu.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("MenuId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("MenuFatherId", typeof(string));
                DataTable.Columns.Add("Order", typeof(string));
                DataTable.Columns.Add("URLPath", typeof(string));
                DataTable.Columns.Add("IconURLPath", typeof(string));
                

                foreach (Menu menu in lstMenu)
                {
                    DataTable.Rows.Add(
                        menu.MenuId,
                        menu.Active,
                        menu.DateTimeCreation,
                        menu.DateTimeLastModification,
                        menu.UserCreationId,
                        menu.UserLastModificationId,
                        menu.Name,
                        menu.MenuFatherId,
                        menu.Order,
                        menu.URLPath,
                        menu.IconURLPath
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
