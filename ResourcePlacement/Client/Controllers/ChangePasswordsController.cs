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
    public class ChangePasswordsController : BaseController<Account, AccountRepository, string>
    {

        private readonly AccountRepository repository;

        public ChangePasswordsController(AccountRepository repository): base(repository)
        {
            this.repository = repository;
        }

        [HttpPut("changepass")]
        public JsonResult ResetPassword(ChangePasswordVM changePasswordVM)
        {
            var result = repository.ChangePassword(changePasswordVM);
            return Json(result);
        }

        [AllowAnonymous]
        [HttpGet("change-page")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
