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
    public class DepartmentRepository:GeneralRepository<Department,int>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public DepartmentRepository(Address address, string request = "Departments/") : base(address, request)
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

        public async Task<Department> GetDepartment(int ID)
        {
            Department department = new Department();
            using (var response = await httpClient.GetAsync(request + ID))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                department= JsonConvert.DeserializeObject<Department>(apiResponse);
            }
            return department;
        }

    }
}
