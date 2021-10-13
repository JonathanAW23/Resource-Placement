using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.ViewModel
{
    public class JobEmployeeVM
    {
        public int Id { get; set; }
        public int IdJob { get; set; }
        public string IdEmployee { get; set; }
        public string TitleJob { get; set; }
        public string Company { get; set; }
        public int Status { get; set; }
        public string FullName { get; set; }
        public DateTime InterviewDate { get; set; }
        public DateTime RecordDate { get; set; }
        public string InterviewTime { get; set; }
        public string Interviewer { get; set; }
        public int InterviewResult { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
