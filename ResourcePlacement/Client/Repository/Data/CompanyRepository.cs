using Client.Base.Url;
using Newtonsoft.Json;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class CompanyRepository:GeneralRepository<Company,int>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public CompanyRepository(Address address, string request = "Companies/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public async Task<Company> GetCompany(int ID)
        {
            Company company = new Company();
            using (var response = await httpClient.GetAsync(request + ID))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                company = JsonConvert.DeserializeObject<Company>(apiResponse);
            }
            return company;
        }

    }
}
