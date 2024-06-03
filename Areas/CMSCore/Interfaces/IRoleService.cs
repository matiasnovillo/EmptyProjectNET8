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
    public interface IRoleService
    {
        void ExportToExcel(string path, DataTable dtRole);

        void ExportToCSV(string path, List<Role> lstRole);

        void ExportToPDF(string path, List<Role> lstRole);

        List<Role> ImportExcel(string path, int userId);
    }
}