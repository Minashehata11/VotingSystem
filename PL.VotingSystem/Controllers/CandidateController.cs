using DAL.VotingSystem.Context;
using System.Security.Claims;

namespace PL.VotingSystem.Controllers
{
    namespace PL.VotingSystem.Controllers
    {

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Candidate")]
        public class CandidateController : BaseController
        {
            #region ImageSettingAllowed
            private List<string> _allowedExtensions = new List<string>() { ".png", ".jpg" };
            private long _maxAllowedPosterSize = 1048576 * 2;
            #endregion
            private readonly ICandidateRepository _candidateRepository;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUnitOfWork _unitOfWork;

            public CandidateController(ApplicationDbContext context, ICandidateRepository candidateRepository, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
            {
                _candidateRepository = candidateRepository;
                _userManager = userManager;
                _unitOfWork = unitOfWork;
            }
            [HttpGet]
            public async Task<ActionResult<CandidateProfileDto>> GetCandidateRecord()
            {
                var email = User?.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email claim not found in JWT token.");
                }

                var user = await _userManager.FindByEmailAsync(email);
                var candidate = await _unitOfWork.candidateRepository.GetByIdWithInclude(user.Id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                string base64Image;


                if (user.Image == null)
                    base64Image = null;

                base64Image = Convert.ToBase64String(user.Image);
                var candidateProfileDto = new CandidateProfileDto
                {
                    Image = base64Image,
                    Name = user.FullName,
                    Birthday = user.DateOfBirth.ToString("yyyy-MM-dd"),
                    Description = candidate.Qulification,
                    Graduate = candidate.Graduate,
                };
                if (user.Gender == Gender.Male)
                    candidateProfileDto.Gender = "Male";
                else
                    candidateProfileDto.Gender = "Female";
                return Ok(candidateProfileDto);
            }
            [HttpGet]
            public async Task<ActionResult<List<PostCandidateDto>>> GetCandidatePosts()
            {
                List<PostCandidateDto> postCandidateDtos = new List<PostCandidateDto>();
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
                var Posts = await _unitOfWork.PosterRepository.GetAllPostsWithCandidateIdAsync(user.Id);
                if (Posts.Any())
                {

                    foreach (var post in Posts)
                    {
                        PostCandidateDto postDto = new PostCandidateDto
                        {
                            PostId = post.PostId,
                            Description = post.Decription,
                            PostImage = Convert.ToBase64String(post.PostImage)
                        };
                        postCandidateDtos.Add(postDto);
                    }

                    return Ok(postCandidateDtos);
                }
                return Ok("Dont Have Posts Yet");
            }
            [HttpDelete]
            public async Task<ActionResult<Post>> DeleteCandidatePosts(int id)
            {
                if (id == 0) return BadRequest("Not Found Id");
                var post = await _unitOfWork.PosterRepository.GetByIdAsycn(id);

                if (post == null)
                    return BadRequest("Not Found Post");
                _unitOfWork.PosterRepository.Delete(post);
                _unitOfWork.Commit();
                return Ok(post);
            }

            [HttpPost]
            public async Task<ActionResult<PostDto>> AddPosts([FromForm] PostDto input)
            {
                var email = User?.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Email claim not found in JWT token.");
                }

                var user = await _userManager.FindByEmailAsync(email);
                var candidate = await _unitOfWork.candidateRepository.GetByIdWithInclude(user.Id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                if (!_allowedExtensions.Contains(Path.GetExtension(input.Post.FileName).ToLower()))
                    return BadRequest("Not Allowed Exitrons");

                if (_maxAllowedPosterSize < input.Post.Length)
                    return BadRequest("Big than 2m");
                using var dataStream = new MemoryStream();
                await input.Post.CopyToAsync(dataStream);

                Post post = new Post
                {
                    CandidateId = candidate.CandidateId,
                    Decription = input.Decription,
                    PostImage = dataStream.ToArray()
                };
                _unitOfWork.PosterRepository.Add(post);
                _unitOfWork.Commit();

                return Ok(input);
            }
            [HttpPut("{postId}")]
            public async Task<ActionResult<PostDto>> UpdatePost(int postId,[FromForm]PostDto input )
            {
                if (postId == 0)
                    return BadRequest("Not found Id");
                var post = await _unitOfWork.PosterRepository.GetByIdAsycn(postId);
                if (post == null)
                    return NotFound("Not Found Posts");
                if (!_allowedExtensions.Contains(Path.GetExtension(input.Post.FileName).ToLower()))
                    return BadRequest("Not Allowed Exitrons");

                if (_maxAllowedPosterSize < input.Post.Length)
                    return BadRequest("Big than 2m");
                using var dataStream = new MemoryStream();
                await input.Post.CopyToAsync(dataStream);

                post.Decription = input.Decription;
                post.PostImage=dataStream.ToArray();
                _unitOfWork.PosterRepository.Update(post); 
                _unitOfWork.Commit(); 
                return Ok(input);
            }
        }
    }
}

