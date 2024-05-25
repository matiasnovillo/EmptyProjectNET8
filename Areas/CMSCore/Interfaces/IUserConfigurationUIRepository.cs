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
    public interface IUserConfigurationUIRepository
    {
        IQueryable<UserConfigurationUI> AsQueryable();

        #region Queries
        int Count();

        UserConfigurationUI? GetByUserConfigurationUIId(int userconfigurationuiId);

        UserConfigurationUI? GetByName(string name);

        List<UserConfigurationUI?> GetAll();

        paginatedUserConfigurationUIDTO GetAllByUserConfigurationUIIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(UserConfigurationUI userconfigurationui);

        bool Update(UserConfigurationUI userconfigurationui);

        bool DeleteByUserConfigurationUIId(int userconfigurationui);
        #endregion

        #region Other methods
        DataTable GetAllInDataTable();
        #endregion
    }
}
