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
            Employee employee = new Employee();
            employee.Id = hrvm.Id;
            employee.FirstName = hrvm.FirstName;
            employee.LastName = hrvm.LastName;
            employee.PhoneNumber = hrvm.PhoneNumber;
            employee.Salary = hrvm.Salary;
            employee.Email = hrvm.Email;
            employee.Gender = hrvm.Gender;
            employee.EmploymentStatus = hrvm.EmploymentStatus;
            employee.DepartmentId = 1;
            myContext.Employees.Add(employee);

            Account account = new Account();
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Id = hrvm.Id;
            account.Password = BCrypt.Net.BCrypt.HashPassword(hrvm.Password, salt);
            myContext.Accounts.Add(account);

            AccountRole accountRole = new AccountRole();
            accountRole.AccountId = hrvm.Id;
            accountRole.RoleId = 1;
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
    }
}
