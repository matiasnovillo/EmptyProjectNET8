using EmptyProject.Areas.CMSCore.Entities;
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

namespace EmptyProject.Areas.CMSCore.Interfaces
{
    public interface IRoleMenuService
    {
        void ExportToExcel(string path, DataTable dtRoleMenu);

        void ExportToCSV(string path, List<RoleMenu> lstRoleMenu);

        void ExportToPDF(string path, List<RoleMenu> lstRoleMenu);

        List<RoleMenu> ImportExcel(string path, int userId);
    }
}