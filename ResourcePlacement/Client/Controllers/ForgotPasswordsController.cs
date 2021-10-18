using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourcePlacement.Model;
using ResourcePlacement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Route("[controller]")]
   
    public class ForgotPasswordsController : BaseController<Account, AccountRepository, string>
    {

        private readonly AccountRepository repository;

        public ForgotPasswordsController(AccountRepository repository): base(repository)
        {
            this.repository = repository;
        }

        [HttpPut("forgotpass")]
        public JsonResult ResetPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var result = repository.ResetPassword(forgotPasswordVM);
            return Json(result);
        }

        [AllowAnonymous]
        [HttpGet("forgot-page")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
