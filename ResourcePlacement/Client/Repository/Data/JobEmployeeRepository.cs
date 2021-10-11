using Client.Base.Url;
using Newtonsoft.Json;
using ResourcePlacement.Model;
using ResourcePlacement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class JobEmployeeRepository:GeneralRepository<JobEmployee,string>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public JobEmployeeRepository(Address address, string request = "JobEmployees/") : base(address, request)
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

        public async Task<List<JobEmployeeVM>> GetJobEmployee()
        {
            List<JobEmployeeVM> entities = new List<JobEmployeeVM>();
            using (var response = await httpClient.GetAsync(request + "GetJobEmployee"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<JobEmployeeVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<JobEmployeeVM>> GetJobEmployeeInvited()
        {
            List<JobEmployeeVM> entities = new List<JobEmployeeVM>();
            using (var response = await httpClient.GetAsync(request + "GetJobEmployeeInvited"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<JobEmployeeVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<JobEmployeeVM>> GetJobEmployeeInterview()
        {
            List<JobEmployeeVM> entities = new List<JobEmployeeVM>();
            using (var response = await httpClient.GetAsync(request + "GetJobEmployeeInterview"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<JobEmployeeVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<JobEmployeeVM>> GetJobEmployeeFinalized()
        {
            List<JobEmployeeVM> entities = new List<JobEmployeeVM>();
            using (var response = await httpClient.GetAsync(request + "GetJobEmployeeFinalized"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<JobEmployeeVM>>(apiResponse);
            }
            return entities;
        }

        public string Register(JobEmployee jobEmployee)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(jobEmployee), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "InsertAssignmentInvitation", content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string ResultDecline(JobEmployee jobEmployee)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(jobEmployee), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request, content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string ResultAccept(JobEmployeeVM jobEmployeevm)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(jobEmployeevm), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request + "InsertJEFinalized", content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

    }
}
