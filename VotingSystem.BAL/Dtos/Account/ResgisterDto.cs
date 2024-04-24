using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLL.VotingSystem.Dtos.Account
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Min Length 6")]
        public string Password { get; set; }

        public string UserName { get; set; }

        public int SSN { get; set; }
        public Gender Gender { get; set; }

        [StringLength(20), MinLength(3)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }
        [MaxLength(40)]
        public string Street { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage ="ImageCard Required")]
        public IFormFile ImageCard { get; set; }

    }
}
