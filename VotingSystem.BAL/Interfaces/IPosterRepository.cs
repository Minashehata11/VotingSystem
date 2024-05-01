using BLL.VotingSystem.Repository;
using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface IPosterRepository:IGenericRepository<Post>
    {
        public Task<Post> GetByIdAsycn(int id);
    }
}
