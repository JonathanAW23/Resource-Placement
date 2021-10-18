using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles ="HR")]
    public class DetailsController : BaseController<JobEmployee, JobEmployeeRepository, string>
    {
        private readonly JobEmployeeRepository repository;
        public DetailsController(JobEmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        
        [HttpGet("detail-assign")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
