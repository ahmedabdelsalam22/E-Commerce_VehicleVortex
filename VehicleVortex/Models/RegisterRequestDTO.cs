﻿using System.ComponentModel.DataAnnotations;

namespace VehicleVortex.Models
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "password must contains 8 characters and numbers at least")]
        public string Password { get; set; }
    }
}
