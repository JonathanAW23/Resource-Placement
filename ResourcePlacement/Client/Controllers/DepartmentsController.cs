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
    public class DepartmentsController : BaseController<Department, DepartmentRepository, int>
    {
        private readonly DepartmentRepository repository;
        public DepartmentsController(DepartmentRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("Page")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
