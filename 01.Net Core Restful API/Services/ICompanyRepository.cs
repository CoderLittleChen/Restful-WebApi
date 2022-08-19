using _01.Net_Core_Restful_API.DtoParameter;
using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Services
{
    public interface ICompanyRepository
    {
        Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameter parameters);

        Task<Company> GetCompanyAsync(Guid companyId);

        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);

        void AddCompany(Company company);

        void UpdateCompany(Company company);

        void DeleteCompany(Company company);

        Task<bool> CompanyExistsAsync(Guid companyId);

        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeDtoParameters parameters);

        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);

        void AddEmployee(Guid companyId, Employee employee);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        Task<bool> SaveAsync();



    }
}
