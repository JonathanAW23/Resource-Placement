using Client.Base.Url;
using Newtonsoft.Json;
using ResourcePlacement.Model;
using ResourcePlacement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class EmployeeRepository:GeneralRepository<Employee,string>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
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

        public async Task<List<Employee>> GetEmployee()
        {
            List<Employee> entities = new List<Employee>();
            using (var response = await httpClient.GetAsync(request+"GetEmployeeOnly/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
            }
            return entities;
        }


        public async Task<List<Employee>> GetHR()
        {
            List<Employee> entities = new List<Employee>();
            using (var response = await httpClient.GetAsync(request + "GetHROnly/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
            }
            return entities;
        }



        public async Task<Employee> GetEmployee(string ID)
        {
            Employee employee= new Employee();
            using (var response = await httpClient.GetAsync(request+ID))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
            }
            return employee;
        }

        public HttpStatusCode DeleteEmployee(string id)
        {
            var result = httpClient.DeleteAsync(request + id).Result;
            return result.StatusCode;
        }


        public string Register(Employee employee)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request, content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }

        public string RegisterHR(HRVM hrvm)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(hrvm), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(request, content).Result.Content.ReadAsStringAsync().Result;
            return result;
        }


    }
}
