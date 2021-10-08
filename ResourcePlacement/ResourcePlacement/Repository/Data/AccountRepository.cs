using ResourcePlacement.Context;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public string CheckEmail(string Email)
        {
            var checkLoginemail = (from e in myContext.Employees 
                                   where e.Email == Email 
                                   select new Employee 
                                   { 
                                       Id = e.Id 
                                   }).ToList();
            if (checkLoginemail.Count == 0)
            {
                return null;
            }
            else
            {
                return checkLoginemail[0].Id;
            }
        }

        public bool CheckPassword(string Id, string password)
        {
            var checkLoginemail = (from e in myContext.Employees
                                   join a in myContext.Accounts on e.Id equals a.Id
                                   where e.Id == Id
                                   select new Account
                                   {
                                       Id = e.Id,
                                       Password = a.Password
                                   }).ToList();
            if (BCrypt.Net.BCrypt.Verify(password, checkLoginemail[0].Password))
                return true;
            else
                return false;
        }

        public string[] GetRole(string Id)
        {
            var getRole = (from a in myContext.Accounts
                           join ar in myContext.AccountRoles on a.Id equals ar.AccountId
                           join r in myContext.Roles on ar.RoleId equals r.Id
                           where a.Id == Id
                           select new Role
                           {
                               Name = r.Name
                           }).ToList();
            string[] roles = new string[getRole.Count];
            for (int i = 0; i < getRole.Count; i++)
            {
                roles[i] = getRole[i].Name;
            }
            return roles;
        }

        public string ResetPasswordGenerator()
        {
            Guid g = Guid.NewGuid();
            string password = g.ToString().Substring(0,12);
            return password;
        }

        public string Email(string password,string email)//tambah string email kalo mau kirim email sesuai email yg di input di model forgot password
        {
            try
            {
                DateTime today = DateTime.Now;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("ercheriom@gmail.com");//email pengirim
                message.To.Add(email);//email penerima (email testing atau string email yg disebut diatas)
                message.Subject = $"Reset Password Request From NETCoreTester {today.Date}";
                message.Body = $"Password anda sudah kami reset menjadi {password}";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ercheriom@gmail.com", "Vongola_123"); //self explanatory
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return "Email berhasil Dikirim";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
