using _01.Net_Core_Restful_API.Data;
using _01.Net_Core_Restful_API.DtoParameter;
using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;

        public CompanyRepository(RoutineDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameter parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = _context.Companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                parameters.CompanyName = parameters.CompanyName.Trim();
                queryExpression = queryExpression.Where(x => x.Name == parameters.CompanyName);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                queryExpression = queryExpression.Where(w => w.Name.Contains(parameters.SearchTerm)
                                                                                || w.Introduction.Contains(parameters.SearchTerm));
            }

            //queryExpression = queryExpression.Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize);

            return await PagedList<Company>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);

        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.FindAsync(companyId);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.Companies.Where(w => companyIds.Contains(w.Id))
                                                            .OrderBy(x => x.Name)
                                                            .ToListAsync();
        }


        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            company.Id = Guid.NewGuid();
            if (company.Employees != null)
            {
                foreach (var item in company.Employees)
                {
                    item.Id = Guid.NewGuid();
                }
            }
            _context.Companies.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            //throw new NotImplementedException();
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companies.Remove(company);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.AnyAsync(a => a.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId,
                EmployeeDtoParameters parameters)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
        
            var items = _context.Employees.Where(x => x.CompanyId == companyId);
            if (!string.IsNullOrWhiteSpace(parameters.Gender))
            {
                var genderStr = parameters.Gender.Trim();
                var gender = Enum.Parse<Gender>(genderStr);
                items = items.Where(w => w.Gender == gender);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Q))
            {
                parameters.Q = parameters.Q.Trim();
                items = items.Where(w => w.EmployeeNo.Contains(parameters.Q)
                                || w.FirstName.Contains(parameters.Q)
                                || w.LastName.Contains(parameters.Q));
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                if (parameters.OrderBy.ToLowerInvariant() == "name")
                {

                    items = items.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
                }
            }


            return await items.OrderBy(x => x.EmployeeNo).ToListAsync();
        }


        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }
            return await _context.Employees.Where(w => w.CompanyId == companyId && w.Id == employeeId)
                                                            .FirstOrDefaultAsync();
        }


        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.CompanyId = companyId;
            //employee.Id = Guid.NewGuid();
            _context.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            //throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.Employees.Remove(employee);
        }


        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }


    }
}
