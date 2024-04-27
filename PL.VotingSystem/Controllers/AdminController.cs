using BLL.VotingSystem.Dtos;
using DAL.VotingSystem.Entities;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Runtime.CompilerServices;

namespace PL.VotingSystem.Controllers
{
   
    [Authorize(AuthenticationSchemes = "Bearer",Roles ="Admin")]
    public class AdminController : BaseController
    {
        #region ImageSettingAllowed
        private List<string> _allowedExtensions = new List<string>() { ".png", ".jpg" };
        private long _maxAllowedPosterSize = 1048576 * 2;
        #endregion
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
            
            var data = _UnitOfWork.AdminRepository.MangeElectionDtos();

            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteElection(int categoryId)
        {
          var voting=await _UnitOfWork.VotingRepository.GetAllVotingByCategoryIdAsync(categoryId);
          var category=  _UnitOfWork.CategoryRepository.GetById(categoryId);
         
            try{
                if (voting.Count()>0) 
                   await _UnitOfWork.VotingRepository.DeleteAllAsync(voting);
                if (category == null)
                    return BadRequest("Not Found Category");
            _UnitOfWork.CategoryRepository.Delete(category);
            _UnitOfWork.Commit();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(voting);
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            if (id == 0)
                return NotFound("Not Found Id");

            var category = await _UnitOfWork.CategoryRepository.GetByIdWithIncludeAsync(id);
            if (category == null) return NotFound("Not Found Category");
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<CreateCategoryDto>> AddNewElection([FromForm] CreateCategoryDto input)
        {
            if (ModelState.IsValid)
            {
                var existingCandidates = await _UnitOfWork.candidateRepository.GetByManyIdsAsync(input.CandidatesId);
                if (input.CategoryLogo == null)
                    return BadRequest("Image Required");
            
                    if (!_allowedExtensions.Contains(Path.GetExtension(input.CategoryLogo.FileName).ToLower()))
                     return BadRequest("Not Allowed Exitrons");

                    if (_maxAllowedPosterSize < input.CategoryLogo.Length)
                        return BadRequest("Big than 2m");
                    using var dataStream = new MemoryStream();
                    await input.CategoryLogo.CopyToAsync(dataStream);


                Category category = new Category
                {
                    CategoryLogo = dataStream.ToArray(),
                    DateOfEndVoting = input.DateOfEndVoting,
                    Name = input.Name,
                    Candidates=existingCandidates
                    
                };
                _UnitOfWork.CategoryRepository.Add(category);
      
                _UnitOfWork.Commit();
                return Ok(category);
            }  
            else
                return BadRequest("Enter All Input");
        }

    }
}
