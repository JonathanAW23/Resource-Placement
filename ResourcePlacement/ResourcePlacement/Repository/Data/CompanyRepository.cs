using ResourcePlacement.Context;
using ResourcePlacement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Repository.Data
{
    public class CompanyRepository : GeneralRepository<MyContext, Company, int>
    {
        public CompanyRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
