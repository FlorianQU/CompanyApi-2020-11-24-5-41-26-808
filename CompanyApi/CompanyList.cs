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
            generateID = 0;
        }

        public List<Company> GetAllCompanies()
        {
            return companies.Select(company => company).ToList();
        }

        public Company GetCompanyByID(string companyID)
        {
            return companies.FirstOrDefault(company => company.CompanyID == companyID);
        }

        public List<Company> GetCompanyByPage(int pageSize, int pageIndex)
        {
            //return companies.Where(company =>
            //{
            //    var companyIndex = long.Parse(company.CompanyID.Split('_')[1]);
            //    return companyIndex > (pageIndex - 1) * pageSize && companyIndex <= pageIndex * pageSize;
            //}).ToList();
            return companies.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        }

        public void DeleteCompanyById(string companyId)
        {
            var company = companies.FirstOrDefault(company => company.CompanyID == companyId);
            if (company != null)
            {
                companies.Remove(company);
            }
        }

        public long GetIndex(Company company)
        {
            return companies.IndexOf(company);
        }

        private string GenerateCompanyId()
        {
            generateID += 1;
            return $"company_{generateID}";
        }
    }
}
