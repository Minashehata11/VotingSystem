using System.Security.Claims;

namespace PL.VotingSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Voter")]

    public class VoterController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public VoterController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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
            if (voter.CategoryId == id)
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdWithIncludeAsync(id);
                if (category == null) return NotFound("Not Found Category");
                return Ok(category);

            }
            else
                return Ok("You don't have permissions to Vote in this Voting");
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
    }
}
