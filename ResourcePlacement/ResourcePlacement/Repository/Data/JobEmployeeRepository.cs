using ResourcePlacement.Context;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class JobEmployeeRepository : GeneralRepository<MyContext, JobEmployee, string>
    {
        public JobEmployeeRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
