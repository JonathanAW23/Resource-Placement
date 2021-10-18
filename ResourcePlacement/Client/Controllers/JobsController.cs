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
   
    public class JobsController : BaseController<Job, JobRepository, int>
    {
        private readonly JobRepository repository;
        public JobsController(JobRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetJob/{ID}")]
        public async Task<JsonResult> GetJob(int ID)
        {
            var result = await repository.GetJob(ID);
            return Json(result);
        }

        [HttpGet("Main")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
