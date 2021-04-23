using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.ViewModels
{
    public class AuthenticateRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
