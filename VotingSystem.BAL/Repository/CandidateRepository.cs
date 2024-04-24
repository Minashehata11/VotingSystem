using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
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

        public async Task<Candidate?> GetCandidateRecord(int id)
        {
            return await _context.Candidates.FindAsync(id);  // Find the Candidate by id 
        }
    }
}
