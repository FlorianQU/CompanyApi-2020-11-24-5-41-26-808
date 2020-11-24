using System;
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
            Company company = new Company("company_1");
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
            List<Company> companies = companyList.GetAllCompanies();
            Assert.Equal(companies, actualCompanies);
        }

        [Fact]
        public async void Should_Get_All_Companies()
        {
            // given
            Company company_1 = new Company("company_1");
            Company company_2 = new Company("company_2");

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
            List<Company> companies = companyList.GetAllCompanies();
            Assert.Equal(companies, actualCompanies);
        }
    }
}
