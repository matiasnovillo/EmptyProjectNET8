using Microsoft.EntityFrameworkCore;
using EmptyProject.DatabaseContexts;
using System.Text.RegularExpressions;
using System.Data;
using EmptyProject.Areas.System.FailureBack.DTOs;
using EmptyProject.Areas.System.FailureBack.Entities;
using EmptyProject.Areas.System.FailureBack.Interfaces;
using EmptyProject.Areas.CMS.UserBack.Entities;

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

namespace EmptyProject.Areas.System.FailureBack.Repositories
{
    public class FailureRepository : IFailureRepository
    {
        protected readonly EmptyProjectContext _context;

        public FailureRepository(EmptyProjectContext context)
        {
            _context = context;
        }

        public IQueryable<Failure> AsQueryable()
        {
            try
            {
                return _context.Failure.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Failure.Count();
            }
            catch (Exception) { throw; }
        }

        public Failure? GetByFailureId(int failureId)
        {
            try
            {
                return _context.Failure
                            .FirstOrDefault(x => x.FailureId == failureId);
            }
            catch (Exception) { throw; }
        }

        public List<Failure?> GetAll()
        {
            try
            {
                return _context.Failure.ToList();
            }
            catch (Exception) { throw; }
        }

        public List<Failure> GetAllByFailureIdForModal(string textToSearch)
        {
            try
            {
                var query = from failure in _context.Failure
                            select new { Failure = failure };

                // Extraemos los resultados en listas separadas
                List<Failure> lstFailure = query.Select(result => result.Failure)
                        .Where(x => x.FailureId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstFailure;
            }
            catch (Exception) { throw; }
        }

        public List<Failure?> GetAllByFailureId(List<int> lstFailureChecked)
        {
            try
            {
                List<Failure?> lstFailure = [];

                foreach (int FailureId in lstFailureChecked)
                {
                    Failure failure = _context.Failure.Where(x => x.FailureId == FailureId).FirstOrDefault();

                    if (failure != null)
                    {
                        lstFailure.Add(failure);
                    }
                }

                return lstFailure;
            }
            catch (Exception) { throw; }
        }

        public paginatedFailureDTO GetAllByFailureIdPaginated(string textToSearch,
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

                int TotalFailure = _context.Failure.Count();

                var query = from failure in _context.Failure
                            join userCreation in _context.User on failure.UserCreationId equals userCreation.UserId
                            join userLastModification in _context.User on failure.UserLastModificationId equals userLastModification.UserId
                            select new { Failure = failure, UserCreation = userCreation, UserLastModification = userLastModification };

                // Extraemos los resultados en listas separadas
                List<Failure> lstFailure = query.Select(result => result.Failure)
                        .Where(x => strictSearch ?
                            words.All(word => x.FailureId.ToString().Contains(word)) :
                            words.Any(word => x.FailureId.ToString().Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = query.Select(result => result.UserCreation).ToList();
                List<User> lstUserLastModification = query.Select(result => result.UserLastModification).ToList();

                return new paginatedFailureDTO
                {
                    lstFailure = lstFailure,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalFailure,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(Failure failure)
        {
            try
            {
                _context.Failure.Add(failure);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Failure failure)
        {
            try
            {
                _context.Failure.Update(failure);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByFailureId(int failureId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.FailureId == failureId)
                        .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Methods for DataTable
        public DataTable GetAllByFailureIdInDataTable(List<int> lstFailureChecked)
        {
            try
            {
                DataTable DataTable = new();
                DataTable.Columns.Add("FailureId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Message", typeof(string));
                DataTable.Columns.Add("EmergencyLevel", typeof(string));
                DataTable.Columns.Add("StackTrace", typeof(string));
                DataTable.Columns.Add("Source", typeof(string));
                DataTable.Columns.Add("Comment", typeof(string));


                foreach (int FailureId in lstFailureChecked)
                {
                    Failure failure = _context.Failure.Where(x => x.FailureId == FailureId).FirstOrDefault();

                    if (failure != null)
                    {
                        DataTable.Rows.Add(
                        failure.FailureId,
                        failure.Active,
                        failure.DateTimeCreation,
                        failure.DateTimeLastModification,
                        failure.UserCreationId,
                        failure.UserLastModificationId,
                        failure.Message,
                        failure.EmergencyLevel,
                        failure.StackTrace,
                        failure.Source,
                        failure.Comment

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
                List<Failure> lstFailure = _context.Failure.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("FailureId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Message", typeof(string));
                DataTable.Columns.Add("EmergencyLevel", typeof(string));
                DataTable.Columns.Add("StackTrace", typeof(string));
                DataTable.Columns.Add("Source", typeof(string));
                DataTable.Columns.Add("Comment", typeof(string));


                foreach (Failure failure in lstFailure)
                {
                    DataTable.Rows.Add(
                        failure.FailureId,
                        failure.Active,
                        failure.DateTimeCreation,
                        failure.DateTimeLastModification,
                        failure.UserCreationId,
                        failure.UserLastModificationId,
                        failure.Message,
                        failure.EmergencyLevel,
                        failure.StackTrace,
                        failure.Source,
                        failure.Comment

                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
