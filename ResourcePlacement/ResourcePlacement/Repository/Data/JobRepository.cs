using ResourcePlacement.Context;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class JobRepository : GeneralRepository<MyContext, Job, int>
    {
        public JobRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
