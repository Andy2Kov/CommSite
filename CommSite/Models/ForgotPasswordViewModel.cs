﻿using System.ComponentModel.DataAnnotations;

namespace PhotoBank.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
