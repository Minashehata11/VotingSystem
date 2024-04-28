using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface IVotingRepository:IGenericRepository<VoterCandidateCategory>
    {
        public Task<List<VoterCandidateCategory>> GetAllVotingByCategoryIdAsync(int categoryId);
        public Task DeleteAllAsync(List<VoterCandidateCategory> data);
    }
}
