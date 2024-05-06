using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLL.VotingSystem.Dtos.Account
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public IFormFile ImageCard { get; set; }

        public IFormFile FaceImage { get; set; }
    }
}
