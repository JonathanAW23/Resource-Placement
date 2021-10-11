using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Model;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
    public class JEFinalizedsController : BaseController<JobEmployee, JobEmployeeRepository, string>
    {
        private readonly JobEmployeeRepository repository;
        public JEFinalizedsController(JobEmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }


        [HttpGet("GetResult")]
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

        [HttpGet("result-form")]
        public IActionResult Form()
        {
            return View();
        }
    }
}
