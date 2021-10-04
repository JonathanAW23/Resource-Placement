using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.Model
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9-+_!@#$%^&*.,?''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(12, ErrorMessage = "Password cannot exceed 12 characters")]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
