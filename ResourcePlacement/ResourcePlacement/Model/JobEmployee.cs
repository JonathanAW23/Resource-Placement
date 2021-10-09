using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Model
{
    [Table("tb_tr_jobs_employees")]
    public class JobEmployee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        public int Status { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime InterviewDate { get; set; }
        public string InterviewTime { get; set; }
        public string Interviewer { get; set; }
        public int InterviewResult { get; set; }

        [JsonIgnore]
        public virtual Job Job { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }
    }
}
