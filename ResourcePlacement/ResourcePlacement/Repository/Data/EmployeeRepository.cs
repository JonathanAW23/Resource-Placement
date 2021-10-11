using ResourcePlacement.Context;
using ResourcePlacement.Model;
using ResourcePlacement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int InsertHR(HRVM hrvm)
        {
            Employee employee = new Employee
            {
                Id = hrvm.Id,
                FirstName = hrvm.FirstName,
                LastName = hrvm.LastName,
                PhoneNumber = hrvm.PhoneNumber,
                Salary = hrvm.Salary,
                Email = hrvm.Email,
                Gender = hrvm.Gender,
                EmploymentStatus = hrvm.EmploymentStatus,
                DepartmentId = 1
            };
            myContext.Employees.Add(employee);

            Account account = new Account();
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Id = hrvm.Id;
            account.Password = BCrypt.Net.BCrypt.HashPassword(hrvm.Password, salt);
            myContext.Accounts.Add(account);

            AccountRole accountRole = new AccountRole
            {
                AccountId = hrvm.Id,
                RoleId = 2
            };
            myContext.AccountRoles.Add(accountRole);

            var insert = myContext.SaveChanges();
            return insert;
        }
        public bool CheckEmail(string Email)
        {
            var checkEmail = (from e in myContext.Employees where e.Email == Email select new Employee { }).ToList();
            if (checkEmail.Count == 0)
                return true;
            return false;
        }
        public bool CheckPhone(string Phone)
        {
            var checkPhone = (from e in myContext.Employees where e.PhoneNumber == Phone select new Employee { }).ToList();
            if (checkPhone.Count == 0)
                return true;
            return false;
        }

        public IEnumerable<Employee> GetEmployeeOnly()
        {

            var getAllEmployee = (from e in myContext.Employees select e.Id ).ToArray();
            var getHREmployee = (from e in myContext.Employees join a in myContext.Accounts on e.Id equals a.Id select  e.Id ).ToArray();

            var getEmployeeOnly = getAllEmployee.Except(getHREmployee).ToArray();

            List<Employee> getEmployeeOnlyList = new List<Employee>();

            foreach (string Id in getEmployeeOnly) 
            {
                var getEmployee = (from e in myContext.Employees
                                   where e.Id == Id
                                   select new Employee
                                   {
                                       Id = e.Id,
                                       FirstName = e.FirstName,
                                       LastName = e.LastName,
                                       Gender = e.Gender,
                                       Email = e.Email,
                                       PhoneNumber = e.PhoneNumber,
                                       Salary = e.Salary,
                                       EmploymentStatus = e.EmploymentStatus,
                                       DepartmentId = e.DepartmentId
                                   }).First();
                getEmployeeOnlyList.Add(getEmployee);
            }

            if (getEmployeeOnlyList.Count == 0)
            {
                return null;
            }
            return getEmployeeOnlyList;
        }

        public IEnumerable<Employee> GetHROnly()
        {

            var getHR = (from e in myContext.Employees
                               join a in myContext.Accounts on e.Id equals a.Id
                               join ac in myContext.AccountRoles on a.Id equals ac.AccountId
                               join r in myContext.Roles on ac.RoleId equals r.Id
                               where ac.RoleId == 2
                               select new Employee
                               {
                                   Id = e.Id,
                                   FirstName = e.FirstName,
                                   LastName = e.LastName,
                                   Gender = e.Gender,
                                   Email = e.Email,
                                   PhoneNumber = e.PhoneNumber,
                                   Salary = e.Salary,
                                   EmploymentStatus = e.EmploymentStatus,
                                   DepartmentId = e.DepartmentId
                               }).ToList();

            if (getHR.Count == 0)
            {
                return null;
            }
            return getHR;
        }

        public IEnumerable<JobHistoryVM> GetEmployeeJobHistories(string Id)
        {

            var getJH= (from e in myContext.Employees
                         join jh in myContext.JobHistories on e.Id equals jh.EmployeeId
                         join j in myContext.Jobs on jh.JobId equals j.Id
                         join c in myContext.Companies on j.CompanyId equals c.Id
                         where e.Id == Id
                         select new JobHistoryVM
                         {
                             EmployeeId = e.Id,
                             FullName = e.FirstName + " " +e.LastName,
                             JobTitle = j.Title,
                             Company = c.Name,
                             StartDate = jh.StartDate,
                             EndDate = jh.EndDate
                         }).ToList();

            if (getJH.Count == 0)
            {
                return null;
            }
            return getJH;
        }

        public IEnumerable<JobEmployeeVM> GetEmployeeJobInterview(string Id)
        {

            var getInterview = (from e in myContext.Employees
                                join je in myContext.JobEmployees on e.Id equals je.EmployeeId
                                join j in myContext.Jobs on je.JobId equals j.Id
                                join c in myContext.Companies on j.CompanyId equals c.Id
                                where e.Id == Id && je.Status == 3
                                select new JobEmployeeVM
                                {
                                    IdEmployee = e.Id,
                                    FullName = e.FirstName + " " + e.LastName,
                                    TitleJob = j.Title,
                                    Company = c.Name,
                                    InterviewDate = je.InterviewDate,
                                    InterviewTime = je.InterviewTime,
                                    Interviewer = je.Interviewer,
                                    InterviewResult = je.InterviewResult
                                }).ToList();

            if (getInterview.Count == 0)
            {
                return null;
            }
            return getInterview;
        }
    }
}
