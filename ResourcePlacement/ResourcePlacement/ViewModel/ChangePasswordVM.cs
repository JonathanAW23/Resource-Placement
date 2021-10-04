using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcePlacement.ViewModel
{
    public class ChangePasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9-+_!@#$%^&*.,?''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(12, ErrorMessage = "Password cannot exceed 12 characters")]
        public string OldPassword { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9-+_!@#$%^&*.,?''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(12, ErrorMessage = "Password cannot exceed 12 characters")]
        public string NewPassword { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9-+_!@#$%^&*.,?''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(12, ErrorMessage = "Password cannot exceed 12 characters")]
        public string NewPasswordConfirm { get; set; }
    }
}
