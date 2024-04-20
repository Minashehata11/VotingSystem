using BLL.VotingSystem.Dtos.Account;
using BLL.VotingSystem.Services;
using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.VotingSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _token;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,ITokenService token,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _token = token;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
                throw new Exception($"Registered {dto.Email} Already Token");
            var appuser = new ApplicationUser
            {
                Email = dto.Email,
                FirstName = dto.UserName,
                UserName = dto.UserName,
                LastName = "",
                DateOfBirth = DateTime.Now,
                SSN = 1545,
                City = "ns",
                Street = "Asdsadas"
            
            };
            var result = await _userManager.CreateAsync(appuser, dto.Password);
            if (!result.Succeeded)
                throw new Exception("Error Faied");
            await _userManager.AddToRoleAsync(appuser, "Voter");
            return new UserDto
            {
                Email = dto.Email,
                DisplayName = dto.UserName,
                Token = _token.GenerateToken(appuser)
            };

        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("User Not Found");
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Faided");
            return new UserDto
            {
                DisplayName = dto.Email.Split('@')[0],
                Email = dto.Email,
                Token = _token.GenerateToken(user)
            };
        }
    }
}
