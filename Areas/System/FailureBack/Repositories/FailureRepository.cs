using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EmptyProject.Areas.CMS.UserBack.Entities;
using EmptyProject.Areas.System.FailureBack.Entities;
using EmptyProject.Areas.System.FailureBack.DTOs;
using EmptyProject.Areas.System.FailureBack.Interfaces;
using EmptyProject.DatabaseContexts;
using System.Text.RegularExpressions;
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

namespace EmptyProject.Areas.System.FailureBack.Repositories
{
    public class FailureRepository : IFailureRepository
    {
        protected readonly EmptyProjectContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions;

        public FailureRepository(EmptyProjectContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
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
                //Look in cache first
                if (!_memoryCache.TryGetValue($@"System.Failure.FailureId={failureId}", out Failure? failure))
                {
                    //If not exist in cache, look in DB
                    failure = _context.Failure
                                .FirstOrDefault(x => x.FailureId == failureId);
                    
                    if (failure != null)
                    {
                        _memoryCache.Set(failureId, failure, _memoryCacheEntryOptions);
                    }
                }
                return failure;
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
                            select new { Failure = failure};

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

                List<Failure> lstFailure = _context.Failure
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.FailureId.ToString().Contains(word)) :
                            words.Any(word => x.FailureId.ToString().Contains(word)))
                        .OrderByDescending(x => x.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (Failure failure in lstFailure)
                {

                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == failure.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == failure.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

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
                EntityEntry<Failure> FailureToAdd = _context.Failure.Add(failure);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    int AddedFailureId = FailureToAdd.Entity.FailureId;

                    _memoryCache.Set($@"System.Failure.FailureId={AddedFailureId}", failure, _memoryCacheEntryOptions);
                }

                return result;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Failure failure)
        {
            try
            {
                _context.Failure.Update(failure);

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Set($@"System.Failure.FailureId={failure.FailureId}", failure, _memoryCacheEntryOptions);
                }

                return result;
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

                bool result = _context.SaveChanges() > 0;

                if (result)
                {
                    _memoryCache.Remove($@"System.Failure.FailureId={failureId}");
                }

                return result;
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
