using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.ViewModel
{
    public class HRVM
    {
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string LastName { get; set; }
        public int Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public int Salary { get; set; }
        public int EmploymentStatus { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9-+_!@#$%^&*.,?''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [MinLength(8)]
        [MaxLength(12)]
        public string Password { get; set; }
    }
}
