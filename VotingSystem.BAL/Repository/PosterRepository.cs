using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Repository
{
    public class PosterRepository : GenericRepository<Post>, IPosterRepository
    {
        private readonly ApplicationDbContext _context;

        public PosterRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Post> GetByIdAsycn(int id)
        =>  await _context.Posts.FindAsync(id);
    }
}
