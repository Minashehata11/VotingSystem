using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAdminRepository AdminRepository { get  ; set; }
        public IVoterRepository voterRepository { get ; set ; }

        public ICandidateRepository candidateRepository { get; set; }
        public IVotingRepository VotingRepository { get; set; }
        public ICategoryRepository CategoryRepository { get ; set ; }
        public IPosterRepository PosterRepository { get ; set ; }

        public UnitOfWork(ApplicationDbContext context)
        {
            AdminRepository = new AdminRepository(context);
            voterRepository = new VoterRepository(context);
            candidateRepository = new CandidateRepository(context);
            VotingRepository = new VotingRepository(context);
            CategoryRepository = new CategoryRepository(context);
            PosterRepository = new PosterRepository(context);
            _context = context;
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
