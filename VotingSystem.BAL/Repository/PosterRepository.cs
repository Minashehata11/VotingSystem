using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.VotingSystem.Repository
{
    public class PosterRepository : GenericRepository<Post>, IPosterRepository
    {
        private readonly ApplicationDbContext _context;

        public PosterRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsWithCandidateIdAsync(string candidateId)
        => await _context.Posts.Where(p=>p.CandidateId==candidateId).ToListAsync();

        public async Task<List<Post>> GetAllPostsWithIncludeAsync()
        => await _context.Posts.Include(p => p.Candidate).ThenInclude(c=>c.User).ToListAsync();

        public async Task<Post> GetByIdAsycn(int id)
        => await _context.Posts.FindAsync(id);
    }
}
