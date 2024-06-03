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
    public interface IParameterService
    {
        void ExportToExcel(string path, DataTable dtParameter);

        void ExportToCSV(string path, List<Parameter> lstParameter);

        void ExportToPDF(string path, List<Parameter> lstParameter);

        List<Parameter> ImportExcel(string path, int userId);
    }
}