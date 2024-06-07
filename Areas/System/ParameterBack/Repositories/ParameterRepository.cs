using Microsoft.EntityFrameworkCore;
using EmptyProject.DatabaseContexts;
using System.Text.RegularExpressions;
using System.Data;
using EmptyProject.Areas.System.ParameterBack.DTOs;
using EmptyProject.Areas.System.ParameterBack.Entities;
using EmptyProject.Areas.System.ParameterBack.Interfaces;
using EmptyProject.Areas.CMS.UserBack.Entities;
using EmptyProject.Areas.System.FailureBack.Entities;

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

namespace EmptyProject.Areas.System.ParameterBack.Repositories
{
    public class ParameterRepository : IParameterRepository
    {
        protected readonly EmptyProjectContext _context;

        public ParameterRepository(EmptyProjectContext context)
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

        public List<Parameter> GetAllByParameterIdForModal(string textToSearch)
        {
            try
            {
                var query = from parameter in _context.Parameter
                            select new { Parameter = parameter };

                // Extraemos los resultados en listas separadas
                List<Parameter> lstParameter = query.Select(result => result.Parameter)
                        .Where(x => x.ParameterId.ToString().Contains(textToSearch))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .ToList();

                return lstParameter;
            }
            catch (Exception) { throw; }
        }

        public List<Parameter?> GetAllByParameterId(List<int> lstParameterChecked)
        {
            try
            {
                List<Parameter?> lstParameter = [];

                foreach (int ParameterId in lstParameterChecked)
                {
                    Parameter parameter = _context.Parameter.Where(x => x.ParameterId == ParameterId).FirstOrDefault();

                    if (parameter != null)
                    {
                        lstParameter.Add(parameter);
                    }
                }

                return lstParameter;
            }
            catch (Exception) { throw; }
        }

        public paginatedParameterDTO GetAllByNamePaginated(string textToSearch,
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

                List<Parameter> lstParameter = _context.Parameter
                        .AsQueryable()
                        .Where(x => strictSearch ?
                            words.All(word => x.Name.Contains(word)) :
                            words.Any(word => x.Name.Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = [];
                List<User> lstUserLastModification = [];

                foreach (Parameter parameter in lstParameter)
                {
                    User UserCreation = _context.User
                        .AsQueryable()
                        .Where(x => x.UserCreationId == parameter.UserCreationId)
                        .FirstOrDefault();

                    lstUserCreation.Add(UserCreation);

                    User UserLastModification = _context.User
                       .AsQueryable()
                       .Where(x => x.UserLastModificationId == parameter.UserLastModificationId)
                       .FirstOrDefault();

                    lstUserLastModification.Add(UserLastModification);
                }

                return new paginatedParameterDTO
                {
                    lstParameter = lstParameter,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
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

        #region Methods for DataTable
        public DataTable GetAllByParameterIdInDataTable(List<int> lstParameterChecked)
        {
            try
            {
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


                foreach (int ParameterId in lstParameterChecked)
                {
                    Parameter parameter = _context.Parameter.Where(x => x.ParameterId == ParameterId).FirstOrDefault();

                    if (parameter != null)
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
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }

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
