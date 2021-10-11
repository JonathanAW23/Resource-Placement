using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Model;
using ResourcePlacement.ViewModel;
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


        [HttpPost("Decline")]
        public JsonResult Register(JobEmployee jobEmployee)
        {
            var result = repository.ResultDecline(jobEmployee);
            return Json(result);
        }


        [HttpPost("Accepted")]
        public JsonResult Result(JobEmployeeVM jobEmployeevm)
        {
            var result = repository.ResultAccept(jobEmployeevm);
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
