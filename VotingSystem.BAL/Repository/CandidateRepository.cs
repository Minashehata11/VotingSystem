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
    public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        public CandidateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Candidate>> GetAllWithInclude()
        => await _context.Candidates.Include(c => c.User).Include(c => c.Posts).ToListAsync(); 

        public async Task<Candidate> GetByIdWithInclude(string id)
        => await _context.Candidates.Include(c=>c.Posts).Include(c=>c.User).SingleOrDefaultAsync(c=>c.CandidateId==id);
        public async Task<List<Candidate>> GetByManyIdsAsync(List<string> candiateIds)
        {
            List<Candidate> candidates = new List<Candidate>();
            foreach (string id in candiateIds)
            {
               var candidate= await _context.Candidates.SingleOrDefaultAsync(x=>x.CandidateId==id);
                if(candidate != null) candidates.Add(candidate);
            }

            return candidates;
        }
        public async Task<List<Candidate>> SearchUserByNameAsync(string? name)
       => await _context.Candidates.Include(v => v.User).Include(c => c.Posts).Where(x => x.User.UserName.Trim().ToLower().Contains(name.Trim().ToLower())).ToListAsync();
    }
}
