using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        static private CompanyList companies = new CompanyList();
        [HttpDelete("clear")]
        public void Clear()
        {
            companies.Clear();
        }

        [HttpPost("companies")]
        public Company AddCompany(Company company)
        {
            if (!companies.ContainsCompany(company))
            {
                companies.AddCompany(company);
            }

            return company;
        }

        [HttpGet("companies")]
        public List<Company> GetAllCompanies()
        {
            return companies.GetAllCompanies();
        }

        [HttpGet("companies/{companyID}")]
        public Company GetCompany(string companyID)
        {
            return companies.GetCompanyByID(companyID);
        }

        [HttpGet("companies/{pageSize}&{pageIndex}")]
        public List<Company> GetCompanyByPage(int pageSize, int pageIndex)
        {
            return companies.GetCompanyByPage(pageSize, pageIndex);
        }

        [HttpPatch("companies/{companyId}")]
        public Company UpdateCompany(string companyId, CompanyUpdateModel updateModel)
        {
            var company = companies.GetCompanyByID(companyId);
            company.Name = updateModel.Name;
            return company;
        }

        [HttpPost("companies/{companyId}/employees")]
        public Employee AddEmployee(string companyId, Employee employee)
        {
            Company company = companies.GetCompanyByID(companyId);
            company.AddEmployee(employee);
            return employee;
        }

        [HttpGet("companies/{companyId}/employees")]
        public List<Employee> GetEmployees(string companyId)
        {
            Company company = companies.GetCompanyByID(companyId);
            return company.GetAllEmployees();
        }

        [HttpGet("companies/{companyId}/employees/{employeeId}")]
        public Employee GetEmployees(string companyId, string employeeId)
        {
            Company company = companies.GetCompanyByID(companyId);
            return company.GetEmployeeById(employeeId);
        }

        [HttpPatch("companies/{companyId}/employees/{employeeId}")]
        public Employee UpdateEmployee(string companyId, string employeeId, EmployeeUpdateModel employeeUpdateModel)
        {
            var company = companies.GetCompanyByID(companyId);
            var employee = company.GetEmployeeById(employeeId);
            employee.Name = employeeUpdateModel.Name;
            employee.Salary = employeeUpdateModel.Salary;
            return employee;
        }

        [HttpDelete("companies/{companyId}/employees/{employeeId}")]
        public void DeleteEmployee(string companyId, string employeeId)
        {
            var company = companies.GetCompanyByID(companyId);
            company.DeleteEmployeeById(employeeId);
        }

        [HttpDelete("companies/{companyId}/employees")]
        public void DeleteEmployees(string companyId)
        {
            var company = companies.GetCompanyByID(companyId);
            company.DeleteEmployees();
        }

        [HttpDelete("companies/{companyId}")]
        public void DeleteCompany(string companyId)
        {
            companies.DeleteCompanyById(companyId);
        }
    }
}
