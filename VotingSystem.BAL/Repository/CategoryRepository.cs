using AutoMapper;
using BLL.VotingSystem.Dtos;
using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<CategoryDto> GetByIdWithIncludeAsync(int id)
        { 
          return  await _context.Categories.Include(x => x.Candidates).ThenInclude(x => x.User).Select(c => new CategoryDto
        {
                      id=c.CategoryId,
                      Name = c.Name,
                     DateOfEndVoting = c.DateOfEndVoting,
                      CategoryLogo = c.CategoryLogo,
                    candidateDtos = c.Candidates.Select(u =>new CandidateDto {Name= u.User.UserName,NumberOfVotes= u.NumberOfVote }).ToList(),
                    
             }).SingleOrDefaultAsync(c => c.id == id);

        }
     
        
    }
}
