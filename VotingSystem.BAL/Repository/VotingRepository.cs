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
    public class VotingRepository :GenericRepository<VoterCandidateCategory>, IVotingRepository
    {
        private readonly ApplicationDbContext _context;

        public VotingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public  async Task DeleteAllAsync(List<VoterCandidateCategory> data)
        =>  _context.voterCandidateCategories.RemoveRange(data);

        public async Task<List<VoterCandidateCategory>> GetAllVotingByCategoryIdAsync(int categoryId)
          => await _context.voterCandidateCategories.Where(c => c.CategoryId == categoryId).ToListAsync();
             

        
    }
}
