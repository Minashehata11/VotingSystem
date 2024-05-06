using BLL.VotingSystem.Dtos.Account;
using BLL.VotingSystem.Services;
using System.Security.Claims;
using System.Text.Json;

namespace PL.VotingSystem.Controllers
{

    public class AccountController : BaseController
    {
        #region ImageSettingAllowed
        private List<string> _allowedExtensions = new List<string>() { ".png", ".jpg" };
        private long _maxAllowedPosterSize = 1048576 * 2;
        #endregion
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _token;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFlaskConsumer _flaskConsumer;

        public AccountController(UserManager<ApplicationUser> userManager, ITokenService token, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, IFlaskConsumer flaskConsumer)
        {
            _userManager = userManager;
            _token = token;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _flaskConsumer = flaskConsumer;
        }


        [HttpPost]
        public async Task<ActionResult<ExtractedData>> GetDataModel([FromForm] DataModelDto dataModelDto)
        {
            var extractedData = await _flaskConsumer.DataExtractionAsync(dataModelDto.ImageCard);
            if (extractedData == null)
                return NotFound();
            var voter = await _unitOfWork.voterRepository.GetByNidAsync(extractedData.NID);
            if (voter != null)
                return BadRequest("SSn Already Found");
            return extractedData;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register([FromForm] RegisterDto dto)
        {

            if (dto == null)
                return BadRequest("No User created");
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.ImageCard.FileName).ToLower()))
                return BadRequest("ONly JPG and Png Allowed");
            if (dto.ImageCard.Length > _maxAllowedPosterSize)
                return BadRequest("Max Allowed is 2M");
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
                return BadRequest($"Registered {dto.Email} Already Token");

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
                City = dto.Address,
                Voter = new Voter
                {
                    ImageCard = dataStream.ToArray(),
                }
            };
            var result = await _userManager.CreateAsync(appuser, dto.Password);
            if (!result.Succeeded)
                return BadRequest(error: "Not Succeded Register");
            await _userManager.AddToRoleAsync(appuser, "Voter");
            return new UserDto
            {
                Id = appuser.Id,
                Email = dto.Email,
                DisplayName = dto.UserName,
                Token = await _token.GenerateToken(appuser)
            };

        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login([FromForm] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return BadRequest("NotFound User");
            }

            // Check for lockout before processing further
            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest("Account Blocked for 1 min");
            }
            JsonElement json = await _flaskConsumer.FaceRecognitionAsync(dto.ImageCard, dto.FaceImage);
            string message;
                // Try accessing the "result" property
                if (json.TryGetProperty("result", out JsonElement resultProperty))
                         message = resultProperty.GetString();
                else 
                    message = string.Empty;
            
            
            if (message == "Face verification successful.")
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
                if (!result.Succeeded)
                {
                    await _userManager.AccessFailedAsync(user);

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        return BadRequest("Locked for 1 min");
                    }
                    else
                    {
                        return BadRequest("Incorrect Password");
                    }

                }
                await _userManager.ResetAccessFailedCountAsync(user);
                return new UserDto
                {
                    Id = user.Id,
                    DisplayName = dto.Email.Split('@')[0],
                    Email = dto.Email,
                    Token = await _token.GenerateToken(user)
                };

            }
            else
                return BadRequest("Face verification Failed ");

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserViewDto>> Profile()
        {
            string base64Image = "";
            var email = User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim not found in JWT token.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            if (user.Image != null)
                base64Image = Convert.ToBase64String(user.Image);

            UserViewDto userView = new UserViewDto
            {
                Name = user.UserName,
                SSN = user.SSN,
                City = user.City,
                DateOfBirth = user.DateOfBirth.ToString("yyyy-mm-dd"),
                Email = user.Email,
                Gender = user.Gender.ToString(),
                ImageProile = base64Image
            };

            return Ok(userView);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();


            return Ok("Logout successful.");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> UpdateProfile([FromForm] RegisterDto dto)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim not found in JWT token.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            else
            {

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.City = dto.Address;
                user.SSN = dto.SSN;
                user.DateOfBirth = dto.DateOfBirth;
                user.UserName = dto.UserName;
                user.Gender = dto.Gender;

                if (dto.ImageCard is not null)
                {
                    if (dto == null)
                        return BadRequest("No User created");
                    if (!_allowedExtensions.Contains(Path.GetExtension(dto.ImageCard.FileName).ToLower()))
                        return BadRequest("ONly JPG and Png Allowed");
                    if (dto.ImageCard.Length > _maxAllowedPosterSize)
                        return BadRequest("Max Allowed is 2M");


                    using var dataStream = new MemoryStream();
                    await dto.ImageCard.CopyToAsync(dataStream);

                    user.Image = dataStream.ToArray();
                }

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded) return BadRequest(result.Errors);

                else return Ok(user);
            }

        }
    }
}
