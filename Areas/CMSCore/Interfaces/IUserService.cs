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
    public interface IUserService
    {
        void ExportToExcel(string path, DataTable dtUser);

        void ExportToCSV(string path, List<User> lstUser);

        void ExportToPDF(string path, List<User> lstUser);

        List<User> ImportExcel(string path, int userId);
    }
}