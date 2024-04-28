using BLL.VotingSystem.Repository;
using DAL.VotingSystem.Context;
using Microsoft.AspNetCore.Mvc;

namespace PL.VotingSystem.Controllers
{
    namespace PL.VotingSystem.Controllers
    {
        [Route("api/candidate")]
        [ApiController]
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
            public async Task<IActionResult> GetCandidateRecord([FromRoute] string CandidateId)
            {
                var data = _candidateRepository.GetById(CandidateId);
                if (data == null)        // check for bad id 
                 
                 {
                     return NotFound();
                 }
               
                return Ok(data);
            }
        }
    }
}
    
