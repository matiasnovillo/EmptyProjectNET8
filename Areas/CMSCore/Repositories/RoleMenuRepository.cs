using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.CMSCore.DTOs;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;

namespace EmptyProject.Areas.CMSCore.Repositories
{
    public class RoleMenuRepository
    {
        protected readonly EFCoreContext _context;

        public RoleMenuRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<RoleMenu> AsQueryable()
        {
            try
            {
                return _context.RoleMenu
                        .AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.RoleMenu
                            .Count();
            }
            catch (Exception) { throw; }
        }

        public RoleMenu? GetById(int roleId)
        {
            try
            {
                return _context.RoleMenu
                        .FirstOrDefault(x => x.RoleMenuId == roleId);
            }
            catch (Exception) { throw; }
        }

        public List<RoleMenu> GetAll()
        {
            try
            {
                return _context.RoleMenu.ToList();
            }
            catch (Exception) { throw; }
        }

        public paginatedRoleMenuDTO GetAllByRoleMenuIdPaginated(string textToSearch,
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

                int TotalRoleMenu = _context.RoleMenu.Count();

                var paginatedRoleMenu = _context.RoleMenu
                        .Where(x => strictSearch ?
                            words.All(word => x.RoleMenuId.ToString().Contains(word)) :
                            words.Any(word => x.RoleMenuId.ToString().Contains(word)))
                        .OrderBy(p => p.RoleMenuId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                return new paginatedRoleMenuDTO
                {
                    lstRoleMenu = paginatedRoleMenu,
                    TotalItems = TotalRoleMenu,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }

        public List<Menu> GetAllByRoleId(int roleId, List<Menu> lstMenu)
        {
            try
            {
                List<RoleMenu> lstRoleMenu = GetAll();

                var lstMenuResult = (from rm in lstRoleMenu
                                     where rm.RoleId == roleId
                                     join m in lstMenu on rm.MenuId equals m.MenuId
                                     select m)
                                          .OrderBy(x => x.Order)
                                          .ToList();

                return lstMenuResult;
            }
            catch (Exception) { throw; }
        }

        public List<MenuWithStateDTO> GetAllWithStateByRoleId(int roleId, List<Menu> lstMenu)
        {
            try
            {
                List<RoleMenu> lstRoleMenu = GetAll();

                var lstMenuWithState = lstMenu
                    .Select(menu =>
                        new MenuWithStateDTO
                        {
                            MenuId = menu.MenuId,
                            Name = menu.Name,
                            MenuFatherId = menu.MenuFatherId,
                            Order = menu.Order,
                            URLPath = menu.URLPath,
                            IconURLPath = menu.IconURLPath,
                            Active = menu.Active,
                            IsSelected = lstRoleMenu
                                .Any(rm => rm.RoleId == roleId && rm.MenuId == menu.MenuId)
                        }
                    ).ToList();

                return lstMenuWithState;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(RoleMenu rolemenu)
        {
            try
            {
                _context.RoleMenu
                .Add(rolemenu);

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(RoleMenu rolemenu)
        {
            try
            {
                _context.RoleMenu
                .Update(rolemenu);

                return _context
                            .SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByRoleMenuId(int rolemenuId)
        {
            try
            {
                AsQueryable()
                .Where(x => x.RoleMenuId == rolemenuId)
                .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByMenuIdAndRoleId(int roleId,
            int menuId)
        {
            try
            {
                AsQueryable()
                .Where(x => x.MenuId == menuId)
                .Where(x => x.RoleId == roleId)
                .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
