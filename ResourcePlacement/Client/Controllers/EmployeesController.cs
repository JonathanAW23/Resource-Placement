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
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {

        private readonly EmployeeRepository repository;

        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetEmployee")]
        public async Task<JsonResult> GetEmployee()
        {
            var result = await repository.GetEmployee();
            return Json(result);
        }

        [HttpGet("GetEmployee/{ID}")]
        public async Task<JsonResult> GetEmployee(string ID)
        {
            var result = await repository.GetEmployee(ID);
            return Json(result);
        }

        [HttpPost("Add")]
        public JsonResult Register(Employee employee)
        {
            var result = repository.Register(employee);
            return Json(result);
        }

        [HttpGet("Main")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
