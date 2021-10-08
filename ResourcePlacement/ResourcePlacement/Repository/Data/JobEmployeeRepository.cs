using ResourcePlacement.Context;
using ResourcePlacement.Model;
using ResourcePlacement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class JobEmployeeRepository : GeneralRepository<MyContext, JobEmployee, string>
    {

        private readonly MyContext myContext;
        public JobEmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<JobEmployeeVM> GetJobEmployeeVM()
        {
            var getJobEmployeeVMs = (from e in myContext.Employees
                                join je in myContext.JobEmployees on e.Id equals je.EmployeeId
                                join j in myContext.Jobs on je.JobId equals j.Id
                                join c in myContext.Companies on j.CompanyId equals c.Id
                                select new JobEmployeeVM
                                {
                                    IdEmployee = e.Id,
                                    FullName = e.FirstName + " " + e.LastName,
                                    IdJob = j.Id,
                                    TitleJob = j.Title,
                                    Company = c.Name,
                                    Status = je.Status,

                                }).ToList();
            if (getJobEmployeeVMs.Count == 0)
            {
                return null;
            }
            return getJobEmployeeVMs;
        }
        public string GetEmail(string Id)
        {
            var getEmail = (from e in myContext.Employees where e.Id == Id select new Employee { Email = e.Email }).ToList();
            if (getEmail.Count == 0)
            {
                return null;
            }
            else
            {
                return getEmail[0].Email;
            }
        }

        public string GetName(string Id)
        {
            var getName = (from e in myContext.Employees where e.Id == Id select new Employee { FirstName = e.FirstName, LastName = e.LastName }).ToList();
            if (getName.Count == 0)
            {
                return null;
            }
            else
            {
                var fullName = getName[0].FirstName + " " + getName[0].LastName;
                return fullName;
            }
        }
        public string GetCompany(int Id)
        {
            var getCompany = (from j in myContext.Jobs 
                           join c in myContext.Companies on j.CompanyId equals c.Id
                           where j.Id == Id select new Company { Name = c.Name }).ToList();
            if (getCompany.Count == 0)
            {
                return null;
            }
            else
            {
                return getCompany[0].Name;
            }
        }

        public string GetJobName(int Id)
        {
            var getJobName = (from j in myContext.Jobs
                              where j.Id == Id
                              select new Job { Title = j.Title }).ToList();
            if (getJobName.Count == 0)
            {
                return null;
            }
            else
            {
                return getJobName[0].Title;
            }
        }

        public string Email(string email, string name, DateTime intdate, string inttime, string user, string jobName, string company)//tambah string email kalo mau kirim email sesuai email yg di input di model forgot password
        {
            try
            {
                DateTime today = DateTime.Now;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("ercheriom@gmail.com");//email pengirim
                message.To.Add(email);//email penerima (email testing atau string email yg disebut diatas)
                message.Subject = $"[Recruit-Me Interview] {name} {today.Date:dd/MM/yyyy}";
                message.Body = $"Dear {name},\n\nYou have been invited for interview on assignment at :\n\nCompany : {company}\nPositon : {jobName}\nDate : {intdate.Date:dd/MM/yyyy}\nTime : {inttime}\nuser : {user}\n\n Please confirm if you can or cannot go to the interview at designated date by contacting this number : +62895687654987 via: WhatsApp\n\nThank you for your time";
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
