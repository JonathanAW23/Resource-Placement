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
    public class CompaniesController : BaseController<Company, CompanyRepository, int>
    {
        private readonly CompanyRepository repository;
        public CompaniesController(CompanyRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetCompany/{ID}")]
        public async Task<JsonResult> GetCompany(int ID)
        {
            var result = await repository.GetCompany(ID);
            return Json(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DelComp/{ID}")]
        public JsonResult DeleteCom(int ID)
        {
            var result = repository.Delete(ID);
            return Json(result);
        }


        [HttpGet("Main")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
