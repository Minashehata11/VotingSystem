using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface IVoterRepository:IGenericRepository<Voter>
    {
        public Task<List<Voter>> GetAllUsersWithIncludeAsync();
        public Task<List<Voter>> SearchUserByNameAsync(string? name);
        public Task<Voter> GetByIdWithIncludeAsync(string id);

        public Task<Voter> GetByNidAsync(string SSN);
    }
}
