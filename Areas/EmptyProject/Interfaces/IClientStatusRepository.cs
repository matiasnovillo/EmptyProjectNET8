using EmptyProject.Areas.EmptyProject.Entities;
using EmptyProject.Areas.EmptyProject.DTOs;
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
    public interface IClientStatusRepository
    {
        IQueryable<ClientStatus> AsQueryable();

        #region Queries
        int Count();

        ClientStatus? GetByClientStatusId(int clientstatusId);

        List<ClientStatus?> GetAll();

        List<ClientStatus> GetAllByClientStatusIdForModal(string textToSearch);

        List<ClientStatus?> GetAllByClientStatusId(List<int> lstClientStatusChecked);

        paginatedClientStatusDTO GetAllByClientStatusIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(ClientStatus clientstatus);

        bool Update(ClientStatus clientstatus);

        bool DeleteByClientStatusId(int clientstatus);
        #endregion

        #region Methods for DataTable
        DataTable GetAllByClientStatusIdInDataTable(List<int> lstClientStatusChecked);

        DataTable GetAllInDataTable();
        #endregion
    }
}
