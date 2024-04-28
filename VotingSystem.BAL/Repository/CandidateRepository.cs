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

    }
}
