﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResourcePlacement.Base;
using ResourcePlacement.Model;
using ResourcePlacement.Repository.Data;
using ResourcePlacement.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePlacement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration configuration;
        public AccountsController(IConfiguration config, AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
            this.configuration = config;
        }

        [HttpPost("Login")]

        public ActionResult Login(LoginVM loginVM)
        {
            string Id = accountRepository.CheckEmail(loginVM.Email);
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound(new JWTokenVM { Token = null, Messages = "Email not found" });
            }
            else if (accountRepository.CheckPassword(Id, loginVM.Password))
            {
                string[] roles = accountRepository.GetRole(Id);
                var claims = new List<Claim>();
                claims.Add(new Claim("Id", Id));
                claims.Add(new Claim("email", loginVM.Email));
                foreach (string role in roles)
                {
                    claims.Add(new Claim("roles", role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                return Ok(new JWTokenVM { Token = new JwtSecurityTokenHandler().WriteToken(token), Messages = "Login success" });
            }
            else
            {
                return Ok(new JWTokenVM { Token = null, Messages = "Wrong password" });
            }
        }

        [HttpPut("ForgotPassword")]

        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            string Id = accountRepository.CheckEmail(forgotPasswordVM.Email);
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound("Email not found");
            }
            else
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                string password = accountRepository.ResetPasswordGenerator();
                Account account = new Account();
                account.Id = Id;
                account.Password = BCrypt.Net.BCrypt.HashPassword(password, salt);
                update(account);
                var sent = accountRepository.Email(password, forgotPasswordVM.Email);
                return Ok(sent);
            }
        }

        [HttpPut("ChangePassword")]

        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            string Id = accountRepository.CheckEmail(changePasswordVM.Email);
            if (string.IsNullOrEmpty(Id))
            {
                return NotFound("Email not found");
            }
            else if (accountRepository.CheckPassword(Id, changePasswordVM.OldPassword))
            {
                if (changePasswordVM.NewPassword == changePasswordVM.NewPasswordConfirm)
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                    Account account = new Account();
                    account.Id = Id;
                    account.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword, salt);
                    update(account);
                    return Ok("Password changed!");
                }
                else 
                {
                    return BadRequest("Your new password confirmation is incorrect");
                }
                
            }
            else
            {
                return BadRequest("Wrong password");
            }
        }
    }
}