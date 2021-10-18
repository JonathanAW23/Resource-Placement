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
    [Authorize]
    public class DepartmentsController : BaseController<Department, DepartmentRepository, int>
    {
        private readonly DepartmentRepository repository;
        public DepartmentsController(DepartmentRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DelDep/{ID}")]
        public JsonResult DeleteDep(int ID)
        {
            var result = repository.Delete(ID);
            return Json(result);
        }

        [HttpGet("Page")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
