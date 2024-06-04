using System.Data;
using EmptyProject.Areas.CMS.RoleMenuBack.DTOs;
using EmptyProject.Areas.CMS.RoleMenuBack.Entities;

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

namespace EmptyProject.Areas.CMS.RoleMenuBack.Interfaces
{
    public interface IRoleMenuRepository
    {
        IQueryable<RoleMenu> AsQueryable();

        #region Queries
        int Count();

        RoleMenu? GetByRoleMenuId(int rolemenuId);

        List<RoleMenu?> GetAll();

        List<RoleMenu?> GetAllByRoleMenuId(List<int> lstRoleMenuChecked);

        List<RoleMenu> GetAllByRoleMenuIdForModal(string textToSearch);

        paginatedRoleMenuDTO GetAllByRoleMenuIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(RoleMenu rolemenu);

        bool Update(RoleMenu rolemenu);

        bool DeleteByRoleMenuId(int rolemenu);
        #endregion

        #region Methods for DataTable
        DataTable GetAllByRoleMenuIdInDataTable(List<int> lstRoleMenuChecked);

        DataTable GetAllInDataTable();
        #endregion
    }
}
