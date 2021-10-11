using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.ViewModel
{
    public class JobHistoryVM
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string JobTitle{ get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

