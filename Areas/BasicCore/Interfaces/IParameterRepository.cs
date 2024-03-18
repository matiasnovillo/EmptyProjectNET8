using EmptyProject.Areas.BasicCore.Entities;
using EmptyProject.Areas.BasicCore.DTOs;
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
    public interface IParameterRepository
    {
        IQueryable<Parameter> AsQueryable();

        #region Queries
        int Count();

        Parameter? GetByParameterId(int testId);

        List<Parameter?> GetAll();

        paginatedParameterDTO GetAllByParameterIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(Parameter test);

        bool Update(Parameter test);

        bool DeleteByParameterId(int testId);
        #endregion

        #region Other methods
        DataTable GetAllInDataTable();
        #endregion
    }
}
