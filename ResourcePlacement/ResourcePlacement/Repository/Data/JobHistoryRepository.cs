using ResourcePlacement.Context;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class JobHistoryRepository : GeneralRepository<MyContext, JobHistory, string>
    {
        public JobHistoryRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
