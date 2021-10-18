using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Model;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "HR")]
    public class JEInterviewsController : BaseController<JobEmployee, JobEmployeeRepository, string>
    {
        private readonly JobEmployeeRepository repository;
        public JEInterviewsController(JobEmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }


        [HttpGet("GetInterview")]
        public async Task<JsonResult> GetJobEmployee()
        {
            var result = await repository.GetJobEmployee();
            return Json(result);
        }


        [HttpGet("Main")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
