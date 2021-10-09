using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
    public class InterviewsController : BaseController<JobEmployee, JobEmployeeRepository, string>
    {
        private readonly JobEmployeeRepository repository;
        public InterviewsController(JobEmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("interview")]
        public JsonResult Register(JobEmployee jobEmployee)
        {
            var result = repository.Register(jobEmployee);
            return Json(result);
        }

        [HttpGet("interview-form")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
