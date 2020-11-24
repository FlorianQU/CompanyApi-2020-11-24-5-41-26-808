﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using CompanyApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CompanyApiTest
{
    public class CompanyApiTest
    {
        private readonly TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        private readonly HttpClient client;
        public CompanyApiTest()
        {
            client = server.CreateClient();
            client.DeleteAsync("company/clear");
        }

        [Fact]
        public async void Should_Add_New_Company()
        {
            // given
            Company company = new Company("company_name_1");
            string request = JsonConvert.SerializeObject(company);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            CompanyList companyList = new CompanyList();

            // when
            await client.PostAsync("company/addCompany", requestBody);
            var response = await client.GetAsync("company/companies");

            // then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<Company> actualCompanies = JsonConvert.DeserializeObject<List<Company>>(responseString);
            companyList.AddCompany(company);
            List<Company> companies = new List<Company>()
            {
                new Company("company_1", "company_name_1"),
            };
            Assert.Equal(companies, actualCompanies);
        }

        [Fact]
        public async void Should_Get_All_Companies()
        {
            // given
            Company company_1 = new Company("company_name_1");
            Company company_2 = new Company("company_name_2");

            string request = JsonConvert.SerializeObject(company_1);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            await client.PostAsync("company/addCompany", requestBody);

            request = JsonConvert.SerializeObject(company_2);
            requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            await client.PostAsync("company/addCompany", requestBody);

            CompanyList companyList = new CompanyList();
            companyList.AddCompany(company_1);
            companyList.AddCompany(company_2);

            // when
            var response = await client.GetAsync("company/companies");

            // then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<Company> actualCompanies = JsonConvert.DeserializeObject<List<Company>>(responseString);
            List<Company> companies = new List<Company>()
            {
                new Company("company_1", "company_name_1"),
                new Company("company_2", "company_name_2"),
            };
            Assert.Equal(companies, actualCompanies);
        }

        [Fact]
        public async void Should_Get_Existing_Company()
        {
            // given
            Company company = new Company("company_name_1");
            string request = JsonConvert.SerializeObject(company);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            // when
            await client.PostAsync("company/addCompany", requestBody);
            string companyID = "company_1";
            var response = await client.GetAsync($"company/companies/{companyID}");

            // then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Company actualCompany = JsonConvert.DeserializeObject<Company>(responseString);
            Company companyQueried = new Company("company_1", "company_name_1");
            Assert.Equal(companyQueried, actualCompany);
        }

        [Fact]
        public async void Should_Get_Company_In_Page()
        {
            // given
            for (int i = 1; i < 11; i++)
            {
                Company company = new Company($"company_name_{i}");
                string request = JsonConvert.SerializeObject(company);
                StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
                await client.PostAsync("company/addCompany", requestBody);
            }

            // when
            long pageSize = 4;
            long pageIndex = 2;
            var response = await client.GetAsync($"company/companies/{pageSize}&{pageIndex}");

            // then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<Company> actualCompany = JsonConvert.DeserializeObject<List<Company>>(responseString);
            List<Company> companyQueried = new List<Company>()
            {
                new Company("company_5", "company_name_5"),
                new Company("company_6", "company_name_6"),
                new Company("company_7", "company_name_7"),
                new Company("company_8", "company_name_8"),
            };
            Assert.Equal(companyQueried, actualCompany);
        }

        [Fact]
        public async void Should_Get_Updated_Company()
        {
            // given
            Company company = new Company("company_name_1");
            string request = JsonConvert.SerializeObject(company);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            // when
            await client.PostAsync("company/addCompany", requestBody);
            string companyID = "company_1";
            string newName = "Changed_Name";
            CompanyUpdateModel updateModel = new CompanyUpdateModel(newName);
            string updateRequest = JsonConvert.SerializeObject(updateModel);
            StringContent updateRequestBody = new StringContent(updateRequest, Encoding.UTF8, "application/json");
            var response = await client.PatchAsync($"company/companies/{companyID}", updateRequestBody);

            // then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Company actualCompany = JsonConvert.DeserializeObject<Company>(responseString);
            Company expectedCompany = new Company(companyID, newName);
            Assert.Equal(expectedCompany, actualCompany);

            var queryResponse = await client.GetAsync($"company/companies/{companyID}");
            var queryResponseString = await queryResponse.Content.ReadAsStringAsync();
            Company queriedCompany = JsonConvert.DeserializeObject<Company>(queryResponseString);
            Assert.Equal(expectedCompany, queriedCompany);
        }
    }
}
