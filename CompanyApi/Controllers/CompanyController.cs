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

        [HttpPost("addCompany")]
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
    }
}
