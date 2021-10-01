using ResourcePlacement.Context;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
