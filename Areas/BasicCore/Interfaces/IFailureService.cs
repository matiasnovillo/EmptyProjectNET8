using EmptyProject.Areas.BasicCore.Entities;
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

namespace EmptyProject.Areas.BasicCore.Interfaces
{
    public interface IFailureService
    {
        void ExportToExcel(string path, DataTable dtFailure);

        void ExportToCSV(string path, List<Failure> lstFailure);

        void ExportToPDF(string path, List<Failure> lstFailure);

        List<Failure> ImportExcel(string path, int userId);
    }
}