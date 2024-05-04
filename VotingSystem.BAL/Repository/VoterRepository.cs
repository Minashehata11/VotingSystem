using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Repository
{
    public class VoterRepository : GenericRepository<Voter>, IVoterRepository
    {
        private readonly ApplicationDbContext _context;

        public VoterRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Voter>> GetAllUsersWithIncludeAsync()
        =>  await _context.Voters.Include(v=>v.User).Include(v=>v.Category).ToListAsync();

        public async Task<Voter> GetByIdWithIncludeAsync(string id)
        => await _context.Voters.Include(v => v.User).Include(v => v.Category).SingleOrDefaultAsync(v => v.VoterId == id);

        public Task<Voter> GetByNidAsync(string SSN)
       => _context.Voters.Include(v => v.User).Where(v => v.User.SSN == SSN).SingleOrDefaultAsync();

        public async  Task<List<Voter>> SearchUserByNameAsync(string? name)
        => await _context.Voters.Include(v=>v.User).Where(x => x.User.UserName.Trim().ToLower().Contains(name.Trim().ToLower())).ToListAsync();
    }
}
