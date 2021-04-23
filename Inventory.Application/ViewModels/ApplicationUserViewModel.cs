using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.ViewModels
{
   public class ApplicationUserViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
