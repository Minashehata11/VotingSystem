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
    public class VoterRepository : GenericRepository<Voter>, IVoterRepository
    {
        public VoterRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
