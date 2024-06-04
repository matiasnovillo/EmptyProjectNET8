using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.DTOs;
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
    public interface IUserRepository
    {
        IQueryable<User> AsQueryable();

        #region Queries
        int Count();

        User? GetByUserId(int userId);

        List<User?> GetAll();

        List<User?> GetAllByUserId(List<int> lstUserChecked);

        List<User> GetAllByUserIdForModal(string textToSearch);

        paginatedUserDTO GetAllByEmailPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(User user);

        bool Update(User user);

        bool DeleteByUserId(int user);
        #endregion

        #region Methods for DataTable
        DataTable GetAllByUserIdInDataTable(List<int> lstUserChecked);

        DataTable GetAllInDataTable();
        #endregion
    }
}
