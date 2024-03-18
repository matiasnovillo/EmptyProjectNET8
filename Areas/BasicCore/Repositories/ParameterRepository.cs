using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmptyProject.Areas.BasicCore.Entities;
using EmptyProject.Areas.BasicCore.Entities.Configuration;
using EmptyProject.Areas.BasicCore.DTOs;
using EmptyProject.Areas.BasicCore.Interfaces;
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

namespace EmptyProject.Areas.BasicCore.Repositories
{
    public class ParameterRepository : IParameterRepository
    {
        protected readonly EFCoreContext _context;

        public ParameterRepository(EFCoreContext context)
        {
            _context = context;
        }

        public IQueryable<Parameter> AsQueryable()
        {
            try
            {
                return _context.Parameter.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Parameter.Count();
            }
            catch (Exception) { throw; }
        }

        public Parameter? GetByParameterId(int parameterId)
        {
            try
            {
                return _context.Parameter
                                .FirstOrDefault(x => x.ParameterId == parameterId);
            }
            catch (Exception) { throw; }
        }

        public List<Parameter?> GetAll()
        {
            try
            {
                return _context.Parameter.ToList();
            }
            catch (Exception) { throw; }
        }

        public paginatedParameterDTO GetAllByParameterIdPaginated(string textToSearch,
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

                int TotalParameter = _context.Parameter.Count();

                var paginatedParameter = _context.Parameter
                        .Where(x => strictSearch ?
                            words.All(word => x.ParameterId.ToString().Contains(word)) :
                            words.Any(word => x.ParameterId.ToString().Contains(word)))
                        .OrderBy(p => p.ParameterId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                return new paginatedParameterDTO
                {
                    lstParameter = paginatedParameter,
                    TotalItems = TotalParameter,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public bool Add(Parameter parameter)
        {
            try
            {
                _context.Parameter.Add(parameter);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(Parameter parameter)
        {
            try
            {
                _context.Parameter.Update(parameter);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByParameterId(int parameterId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.ParameterId == parameterId)
                        .ExecuteDelete();

                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Other methods
        public DataTable GetAllInDataTable()
        {
            try
            {
                List<Parameter> lstParameter = _context.Parameter.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("ParameterId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("Value", typeof(string));
                DataTable.Columns.Add("IsPrivate", typeof(string));
                

                foreach (Parameter parameter in lstParameter)
                {
                    DataTable.Rows.Add(
                        parameter.ParameterId,
                        parameter.Active,
                        parameter.DateTimeCreation,
                        parameter.DateTimeLastModification,
                        parameter.UserCreationId,
                        parameter.UserLastModificationId,
                        parameter.Name,
                        parameter.Value,
                        parameter.IsPrivate
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
