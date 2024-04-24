using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.VotingSystem.Controllers
{
   
    [Authorize(AuthenticationSchemes = "Bearer",Roles ="Admin")]
    public class AdminController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UnitOfWork;

        public AdminController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Home()
        {
         
          var data=_UnitOfWork.AdminRepository.GetHomeData();
            
            return Ok(data);
        }
        [HttpGet("ManageElection")]
        public async Task<IActionResult> ManageElection()
        {

            //TODO:Test Controller  When Make Register(Have User in Database)
            var data = _UnitOfWork.AdminRepository.MangeElectionDtos();

            return Ok(data);
        }

    }
}
