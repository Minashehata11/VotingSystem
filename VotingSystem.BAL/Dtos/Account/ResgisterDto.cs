using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLL.VotingSystem.Dtos.Account
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6,ErrorMessage ="Min Length 6")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserName { get; set; }

        public string SSN { get; set; }
        public Gender Gender { get; set; }

        [StringLength(20), MinLength(3)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile? ImageCard { get; set; }

    }
}
