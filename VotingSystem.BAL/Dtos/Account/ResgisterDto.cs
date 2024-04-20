using System.ComponentModel.DataAnnotations;

namespace BLL.VotingSystem.Dtos.Account
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string UserName { get; set; }

    }
}
