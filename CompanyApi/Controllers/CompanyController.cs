﻿using System;
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
        public List<Company> GetCompanyByPage(long pageSize, long pageIndex)
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
    }
}
