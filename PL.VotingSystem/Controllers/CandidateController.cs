using BLL.VotingSystem.Repository;
using BLL.VotingSystem.Dtos;
using DAL.VotingSystem.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PL.VotingSystem.Controllers
{
    namespace PL.VotingSystem.Controllers
    {
        [Route("api/candidate")]
        [ApiController]
        
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Candidate ")]
        public class CandidateController : ControllerBase
        {
            private readonly ICandidateRepository _candidateRepository;
            private readonly ApplicationDbContext _context; // for more security  
            private string data;

            public CandidateController(ApplicationDbContext context, ICandidateRepository candidateRepository)
            {
                _candidateRepository = candidateRepository;
                _context = context;     // assigining 
            }
            [HttpGet("{CandidateId}")]
            public async Task<IActionResult> GetCandidateRecord([FromRoute] int CandidateId)
            {
                var data = _candidateRepository.GetById(CandidateId);
                if (data == null)        // check for bad id 
                 
                 {
                     return NotFound();
                 }
                var candidateProfileDto = new CandidateProfileDto {
                    Image=data.User.Image,
                    Name = data.User.FullName ,
                    Birthday = data.User.DateOfBirth ,
                    Gender=data.User.Gender ,
                    Description=data.Qulification,
                    Graduate=data.Graduate,
                    Job=data.Jop,
                    Posts=data.Posts
                    
                } ;
               
                return Ok(candidateProfileDto);
            }
        }
    }
}
    
