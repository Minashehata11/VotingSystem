using BLL.VotingSystem.Services;
using System.Security.Claims;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace PL.VotingSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Voter")]

    public class VoterController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFilerFunction _filerFunction;

        public VoterController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,IFilerFunction filerFunction)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _filerFunction = filerFunction;
        }
        [HttpGet("ManageElection")]
        public async Task<ActionResult<List<ViewMangeElectionDto>>> ManageElection()
        {

            var data = await _unitOfWork.AdminRepository.MangeElectionDtos();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            if (id == 0)
                return NotFound("Not Found Id");
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return BadRequest("Not authorize");
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest("Not Found User");
            Voter voter = _unitOfWork.voterRepository.GetById(user.Id);
            if (voter == null) return BadRequest("Not Authorize");
              var category = await _unitOfWork.CategoryRepository.GetById(id);
            if(category.Name== "Presidential election")
            {
                if (category.DateOfEndVoting < DateTime.Now || category.DateOfEndVoting == DateTime.Now)
                    return BadRequest($"Voting was Ended in {category.DateOfEndVoting}");
                var categoryDto = await _unitOfWork.CategoryRepository.GetByIdWithIncludeAsync(id);
                if (categoryDto == null) return NotFound("Not Found Category");

                return Ok(categoryDto);
            }
            else
            {
                if (_filerFunction.StringInXml("myfilterL.xml", user.SSN))
                {
                    //Check Time Of Voting Ended Or NOt Before Show CategoryOfVoting
                    if (category.DateOfEndVoting < DateTime.Now || category.DateOfEndVoting == DateTime.Now)
                        return BadRequest($"Voting was Ended in {category.DateOfEndVoting}");
                    var categoryDto = await _unitOfWork.CategoryRepository.GetByIdWithIncludeAsync(id);
                    if (categoryDto == null) return NotFound("Not Found Category");

                    return Ok(categoryDto);

                }
                else
                    return Ok("You don't have permissions to Vote in this Voting");
            }

            
        }

        [HttpPost("{categoryId}")]
        public async Task<ActionResult<VoterCandidateCategory>> AddVote(int categoryId, string candidateId)
        {
            if(candidateId == null) 
                return BadRequest("Choose Candidate or Cancel");
            var candidate=_unitOfWork.candidateRepository.GetById(candidateId);
            if (candidate == null)
                return BadRequest("Not Found Candidate");

            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
                return BadRequest("Not authorize");
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest("Not Found User");
            var Votings = _unitOfWork.VotingRepository.GetAll();
            if (Votings.Count()!=0)
            {
                foreach (var vote in Votings)
                {
                    if (vote.CategoryId == categoryId && vote.VoterId == user.Id)
                        return BadRequest($"You are already Voting in this category ");
                }

            }
            VoterCandidateCategory voting = new VoterCandidateCategory
            {
                CategoryId = categoryId,
                VoterId = user.Id,
                CandidateId = candidateId,
               
                
            };
            try{
            _unitOfWork.VotingRepository.Add(voting);
            voting.Candidate.NumberOfVote += 1;
            _unitOfWork.Commit();
            return Ok();

            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            



        }
        [HttpGet]
        public async Task<ActionResult<List<PostDtoView>>> GetAllPostsOfCandidates()
        {
            List<PostDtoView> postDtoViews = new List<PostDtoView>();
            var posts = await _unitOfWork.PosterRepository.GetAllPostsWithIncludeAsync();
            if (posts.Any())
            {
                foreach (var post in posts)
                {
                    PostDtoView postDtoView = new PostDtoView
                    {
                        Image = Convert.ToBase64String(post.PostImage),
                        Decription = post.Decription,
                        FullName = post.Candidate.User.FullName,
                        PostId = post.PostId,
                        Qualfication = post.Candidate.Qulification
                    };
                    postDtoViews.Add(postDtoView);
                }
                return Ok(postDtoViews);
            }
            return BadRequest("Not Posts Found");
        }
    }
}
