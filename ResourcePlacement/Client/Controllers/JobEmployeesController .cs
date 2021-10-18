using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Model;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles= "HR")]
    public class JobEmployeesController : BaseController<JobEmployee, JobEmployeeRepository, string>
    {
        private readonly JobEmployeeRepository repository;
        public JobEmployeesController(JobEmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }


        [HttpGet("GetJobEmployee")]
        public async Task<JsonResult> GetJobEmployee()
        {
            var result = await repository.GetJobEmployee();
            return Json(result);
        }

        [HttpGet("GetInterview/{id}")]
        public async Task<JsonResult> GetInterview(string id)
        {
            var result = await repository.GetEmployeeInterview(id);
            return Json(result);
        }


        [HttpGet("GetJobHistory/{id}")]
        public async Task<JsonResult> GetJobHistory(string id)
        {
            var result = await repository.GetJobHistory(id);
            return Json(result);
        }

        [HttpGet("{ID}")]
        public async Task<JsonResult> GetJE(int ID)
        {
            var result = await repository.GetJE(ID);
            return Json(result);
        }

        [HttpGet("Invited")]
        public async Task<JsonResult> Invited()
        {
            var result = await repository.GetJobEmployeeInvited();
            return Json(result);
        }

        [HttpGet("InvitedFiltered")]
        public async Task<JsonResult> InvitedFiltered()
        {
            var result = await repository.GetJobEmployeeInvitedFiltered();
            return Json(result);
        }

        [HttpGet("Interview")]
        public async Task<JsonResult> Interview()
        {
            var result = await repository.GetJobEmployeeInterview();
            return Json(result);
        }

        [HttpGet("InterviewFiltered")]
        public async Task<JsonResult> InterviewFiltered()
        {
            var result = await repository.GetJobEmployeeInterviewFiltered();
            return Json(result);
        }

        [HttpGet("Finalized")]
        public async Task<JsonResult> Finalized()
        {
            var result = await repository.GetJobEmployeeFinalized();
            return Json(result);
        }


        [HttpGet("Main")]
        public IActionResult Index()
        {
            return View();
        }
    }
}