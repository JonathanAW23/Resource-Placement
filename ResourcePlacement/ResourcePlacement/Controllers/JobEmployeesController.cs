using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Base;
using ResourcePlacement.Model;
using ResourcePlacement.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobEmployeesController : BaseController<JobEmployee, JobEmployeeRepository, string>
    {
        private readonly JobEmployeeRepository jobEmployeeRepository;
        public JobEmployeesController(JobEmployeeRepository repository) : base(repository)
        {
            this.jobEmployeeRepository = repository;
        }


        [HttpGet("GetJobEmployee")]
        public ActionResult GetJobEmployeeVM()
        {
            var jobEmployees = jobEmployeeRepository.GetJobEmployeeVM();
            if (jobEmployees == null)
            {
                return NotFound(jobEmployees);
            }
            else
            {
                return Ok(jobEmployees);
            }
        }

        [HttpPost("InsertAssignmentInvitation")]
        public ActionResult InsertAssignmentInvitation(JobEmployee jobEmployee)
        {
            try
            {
                Insert(jobEmployee);
                var email = jobEmployeeRepository.GetEmail(jobEmployee.EmployeeId);
                var name = jobEmployeeRepository.GetName(jobEmployee.EmployeeId);
                var jobName = jobEmployeeRepository.GetJobName(jobEmployee.JobId);
                var company = jobEmployeeRepository.GetCompany(jobEmployee.JobId);
                var sendinvit = jobEmployeeRepository.Email(email, name, jobEmployee.InterviewDate, jobEmployee.InterviewTime, jobEmployee.Interviewer, jobName, company);
                return Ok("Sukses Insert Data");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
