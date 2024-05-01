using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface ICandidateRepository:IGenericRepository<Candidate>
    {
        public Task<List<Candidate>> GetByManyIdsAsync(List<string> candiateIds);
        public Task<Candidate> GetByIdWithInclude(string id);

        public Task<List<Candidate>> GetAllWithInclude();
        public Task<List<Candidate>> SearchUserByNameAsync(string? name);

    }
}
