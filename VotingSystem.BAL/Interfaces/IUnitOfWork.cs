using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface IUnitOfWork
    {
        public IAdminRepository AdminRepository { get; set; }

        public IVoterRepository voterRepository { get; set; }

        public ICandidateRepository candidateRepository { get; set; }
        public IVotingRepository VotingRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }

        public IPosterRepository PosterRepository { get; set; }

        public int Commit();
    }
}
