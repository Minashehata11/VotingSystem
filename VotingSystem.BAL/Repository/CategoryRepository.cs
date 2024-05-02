using AutoMapper;
using BLL.VotingSystem.Dtos;
using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.VotingSystem.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetById(int id)
        => await _context.Categories.FindAsync(id);

        public async Task<CategoryDto> GetByIdWithIncludeAsync(int id)
        {

            return await _context.Categories.Include(x => x.Candidates).ThenInclude(x => x.User).Select(c => new CategoryDto
        {
                      id=c.CategoryId,
                      Name = c.Name,
                     DateOfEndVoting = c.DateOfEndVoting.ToString("yyyy-mm-dd"),
                      CategoryLogo = c.CategoryLogo != null ? Convert.ToBase64String(c.CategoryLogo) : null, 
                    candidateDtos = c.Candidates.Select(u =>new CandidateDto {Name= u.User.UserName,NumberOfVotes= u.NumberOfVote,Id=u.CandidateId }).ToList(),
                    
             }).SingleOrDefaultAsync(c => c.id == id);

        }
     
        
    }
}
