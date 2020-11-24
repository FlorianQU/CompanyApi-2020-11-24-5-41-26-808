﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApi
{
    public class CompanyList
    {
        private List<Company> companies = new List<Company>();
        private long generateID = 0;

        public bool ContainsCompany(Company company)
        {
            return companies.Any(comp => comp.Name == company.Name);
        }

        public void AddCompany(Company company)
        {
            if (!ContainsCompany(company))
            {
                company.CompanyID = GenerateCompanyId();
                companies.Add(company);
            }
        }

        public void Clear()
        {
            companies.Clear();
        }

        public List<Company> GetAllCompanies()
        {
            return companies.Select(company => company).ToList();
        }

        private string GenerateCompanyId()
        {
            generateID += 1;
            return $"company_{generateID}";
        }
    }
}