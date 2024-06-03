using EmptyProject.Areas.EmptyProject.Entities;
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

namespace EmptyProject.Areas.EmptyProject.Interfaces
{
    public interface IClientStatusService
    {
        void ExportToExcel(string path, DataTable dtClientStatus);

        void ExportToCSV(string path, List<ClientStatus> lstClientStatus);

        void ExportToPDF(string path, List<ClientStatus> lstClientStatus);

        List<ClientStatus> ImportExcel(string path, int userId);
    }
}