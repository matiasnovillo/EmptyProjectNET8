using Microsoft.EntityFrameworkCore;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.EmptyProject.Entities;
using EmptyProject.Areas.EmptyProject.DTOs;
using EmptyProject.Areas.EmptyProject.Interfaces;
using EmptyProject.DatabaseContexts;
using System.Text.RegularExpressions;
using System.Data;
using DocumentFormat.OpenXml.ExtendedProperties;

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

namespace EmptyProject.Areas.EmptyProject.Repositories
{
    public class ClientStatusRepository : IClientStatusRepository
    {
        protected readonly EmptyProjectContext _context;

        public ClientStatusRepository(EmptyProjectContext context)
        {
            _context = context;
        }

        public IQueryable<ClientStatus> AsQueryable()
        {
            try
            {
                return _context.ClientStatus.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.ClientStatus.Count();
            }
            catch (Exception) { throw; }
        }

        public ClientStatus? GetByClientStatusId(int clientstatusId)
        {
            try
            {
                return _context.ClientStatus
                            .FirstOrDefault(x => x.ClientStatusId == clientstatusId);
            }
            catch (Exception) { throw; }
        }

        public List<ClientStatus?> GetAll()
        {
            try
            {
                return _context.ClientStatus.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<ClientStatus?> GetAllByClientStatusId(List<int> lstClientStatusChecked)
        {
            try
            {
                List<ClientStatus?> lstClientStatus = [];

                foreach (int ClientStatusId in lstClientStatusChecked)
                {
                    ClientStatus clientstatus = _context.ClientStatus.Where(x => x.ClientStatusId == ClientStatusId).FirstOrDefault();

                    if (clientstatus != null)
                    {
                        lstClientStatus.Add(clientstatus);
                    }
                }

                return lstClientStatus;
            }
            catch (Exception) { throw; }
        }

        public List<ClientStatus> GetAllByClientStatusIdForModal(string textToSearch)
        {
            try
            {
                var query = from clientstatus in _context.ClientStatus
                            select new { ClientStatus = clientstatus};

                // Extraemos los resultados en listas separadas
                List<ClientStatus> lstClientStatus = query.Select(result => result.ClientStatus)
                        .Where(x => x.ClientStatusId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstClientStatus;
            }
            catch (Exception) { throw; }
        }

        public paginatedClientStatusDTO GetAllByClientStatusIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex, 
            int pageSize)
        {
            try
            {
                //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
                string[] words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                int TotalClientStatus = _context.ClientStatus.Count();

                var query = from clientstatus in _context.ClientStatus
                            join userCreation in _context.User on clientstatus.UserCreationId equals userCreation.UserId
                            join userLastModification in _context.User on clientstatus.UserLastModificationId equals userLastModification.UserId
                            select new { ClientStatus = clientstatus, UserCreation = userCreation, UserLastModification = userLastModification };

                // Extraemos los resultados en listas separadas
                List<ClientStatus> lstClientStatus = query.Select(result => result.ClientStatus)
                        .Where(x => strictSearch ?
                            words.All(word => x.ClientStatusId.ToString().Contains(word)) :
                            words.Any(word => x.ClientStatusId.ToString().Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = query.Select(result => result.UserCreation).ToList();
                List<User> lstUserLastModification = query.Select(result => result.UserLastModification).ToList();

                return new paginatedClientStatusDTO
                {
                    lstClientStatus = lstClientStatus,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalClientStatus,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(ClientStatus clientstatus)
        {
            try
            {
                _context.ClientStatus.Add(clientstatus);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(ClientStatus clientstatus)
        {
            try
            {
                _context.ClientStatus.Update(clientstatus);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByClientStatusId(int clientstatusId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.ClientStatusId == clientstatusId)
                        .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByClientStatusIdInDataTable(List<int> lstClientStatusChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("ClientStatusId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (int ClientStatusId in lstClientStatusChecked)
                {
                    ClientStatus clientstatus = _context.ClientStatus.Where(x => x.ClientStatusId == ClientStatusId).FirstOrDefault();

                    if (clientstatus != null)
                    {
                        DataTable.Rows.Add(
                        clientstatus.ClientStatusId,
                        clientstatus.Active,
                        clientstatus.DateTimeCreation,
                        clientstatus.DateTimeLastModification,
                        clientstatus.UserCreationId,
                        clientstatus.UserLastModificationId,
                        clientstatus.Name
                        
                        );
                    }
                }                

                return DataTable;
            }
            catch (Exception) { throw; }
        }

        public DataTable GetAllInDataTable()
        {
            try
            {
                List<ClientStatus> lstClientStatus = _context.ClientStatus.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("ClientStatusId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                

                foreach (ClientStatus clientstatus in lstClientStatus)
                {
                    DataTable.Rows.Add(
                        clientstatus.ClientStatusId,
                        clientstatus.Active,
                        clientstatus.DateTimeCreation,
                        clientstatus.DateTimeLastModification,
                        clientstatus.UserCreationId,
                        clientstatus.UserLastModificationId,
                        clientstatus.Name
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
