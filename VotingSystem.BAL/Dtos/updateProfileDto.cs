using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class updateProfileDto
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(100, ErrorMessage = "The password must be at least {6} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Old  password.")]
        [DataType(DataType.Password)]
        
        public string OldPassword { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public Gender Gender { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile ImageProfile { get; set; }
    }
}
