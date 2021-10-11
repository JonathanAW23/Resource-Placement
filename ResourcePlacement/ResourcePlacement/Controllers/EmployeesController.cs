using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Base;
using ResourcePlacement.Model;
using ResourcePlacement.Repository.Data;
using ResourcePlacement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        [HttpGet("GetEmployeeOnly")]
        public ActionResult GetEmployeeOnly()
        {
            var EmployeesOnly = employeeRepository.GetEmployeeOnly();
            if (EmployeesOnly == null)
            {
                return NotFound(EmployeesOnly);
            }
            else
            {
                return Ok(EmployeesOnly);
            }
        }

        [HttpGet("GetHROnly")]
        public ActionResult GetHROnly()
        {
            var HROnly = employeeRepository.GetHROnly();
            if (HROnly == null)
            {
                return NotFound(HROnly);
            }
            else
            {
                return Ok(HROnly);
            }
        }

        [HttpGet("GetEmployeeJobHistories/{Id}")]
        public ActionResult GetEmployeeJobHistories(string Id)
        {
            var EmployeeJH = employeeRepository.GetEmployeeJobHistories(Id);
            if (EmployeeJH == null)
            {
                return NotFound(EmployeeJH);
            }
            else
            {
                return Ok(EmployeeJH);
            }
        }

        [HttpGet("GetEmployeeInterviewHistories/{Id}")]
        public ActionResult GetEmployeeInterviewHistories(string Id)
        {
            var EmployeeIH = employeeRepository.GetEmployeeJobInterview(Id);
            if (EmployeeIH == null)
            {
                return NotFound(EmployeeIH);
            }
            else
            {
                return Ok(EmployeeIH);
            }
        }

        [HttpPost("InsertHR")]
        public ActionResult InsertHR(HRVM hrvm)
        {
            try
            {
                if (employeeRepository.CheckEmail(hrvm.Email) == false)
                {
                    return BadRequest("Email sudah terdaftar");
                }
                else if (employeeRepository.CheckPhone(hrvm.PhoneNumber) == false)
                {
                    return BadRequest("Phone Number sudah terdaftar");
                }
                else 
                {
                    employeeRepository.InsertHR(hrvm);
                    return Ok("Sukses Insert Data");
                }    
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


     
    }


}
