using Client.Base.Controllers;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class LoginsController : BaseController<Account, LoginRepository, string>
    {
        private readonly LoginRepository repository;

        public LoginsController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(string email, string password)
        {
            var login = new LoginVM
            {
                Email = email,
                Password = password
            };
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;
            var user = jwtToken.FullName;

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("Email", email);

            HttpContext.Session.SetString("User", user);

            return RedirectToAction("Main", "Employees");
        }

        [AllowAnonymous]
        [HttpGet("login-page")]
        public IActionResult Index()
        {
            //ViewBag.username=null;
            return View();
        }

        [Authorize]
        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

    }
}
