using EmptyProject.Areas.CMS.MenuBack.Entities;
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

namespace EmptyProject.Areas.CMS.MenuBack.Interfaces
{
    public interface IMenuService
    {
        void ExportToExcel(string path, DataTable dtMenu);

        void ExportToCSV(string path, List<Menu> lstMenu);

        void ExportToPDF(string path, List<Menu> lstMenu);

        List<Menu> ImportExcel(string path, int userId);
    }
}