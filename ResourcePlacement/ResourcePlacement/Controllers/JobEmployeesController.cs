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
        public JobEmployeesController(JobEmployeeRepository repository) : base(repository)
        {
        }
    }
}
