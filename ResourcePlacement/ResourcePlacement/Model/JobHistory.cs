using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Model
{
    [Table("tb_tr_job_histories")]
    public class JobHistory
    {
        public string EmployeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int JobId { get; set; }

        [JsonIgnore]
        public virtual Job Job { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
    }
}
