using BLL.VotingSystem.Dtos.Account;
using BLL.VotingSystem.Services;
using DAL.VotingSystem.Entities;
using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PL.VotingSystem.Controllers
{
    
    public class AccountController : BaseController
    {
        #region ImageSettingAllowed
           private List<string> _allowedExtensions = new List<string>() {".png",".jpg" };
           private long _maxAllowedPosterSize = 1048576*2;
        #endregion
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
        public async Task<ActionResult<UserDto>> Register([FromForm]RegisterDto dto)
        {
           
            if (dto == null)
                return BadRequest("No User created");
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.ImageCard.FileName).ToLower()))
                return BadRequest("ONly JPG and Png Allowed");
            if (dto.ImageCard.Length > _maxAllowedPosterSize)
                return BadRequest("Max Allowed is 2M");
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
                return  BadRequest($"Registered {dto.Email} Already Token");

              using var dataStream = new MemoryStream();
              await dto.ImageCard.CopyToAsync(dataStream);
              
            var appuser = new ApplicationUser
            {
                Email = dto.Email,
                FirstName = dto.UserName,
                UserName = dto.UserName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                SSN = dto.SSN,
                City = dto.City,
                Street = dto.Street,
                Voter = new Voter
                {
                    ImageCard=dataStream.ToArray(),
                }
            };
            var result = await _userManager.CreateAsync(appuser, dto.Password);
            if (!result.Succeeded)
                return BadRequest(error: "Not Succeded Register");
            await _userManager.AddToRoleAsync(appuser, "Voter");
           var role=await _userManager.GetRolesAsync(appuser);
            return new UserDto
            {
                Email = dto.Email,
                DisplayName = dto.UserName,
                Token = await _token.GenerateToken(appuser)
            };

        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("NotFound User");
            if (await _userManager.IsLockedOutAsync(user))
            {
                ModelState.AddModelError(string.Empty, "Your account is locked out. Please try again later.");
                return BadRequest("Account Blocked for 1 min");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                if (!await _userManager.IsLockedOutAsync(user))
                {
                    await _userManager.AccessFailedAsync(user);
                }

                // Check for lockout after attempted login
                if (await _userManager.IsLockedOutAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked out for 1 minute due to multiple failed login attempts. Please try again later.");
                    return BadRequest("Locked for 1 min");
                }
                else
                    return BadRequest("Incorrect Password");

            }
            else
            {
                await _userManager.ResetAccessFailedCountAsync(user);
            return new UserDto
            {
                DisplayName = dto.Email.Split('@')[0],
                Email = dto.Email,
                Token = await _token.GenerateToken(user)
            };

            }
        }
    }
}
