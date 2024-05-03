using DAL.VotingSystem.Entities;

namespace BLL.VotingSystem.Interfaces
{
    public interface IPosterRepository:IGenericRepository<Post>
    {
        public Task<Post> GetByIdAsycn(int id);
        public Task<List<Post>> GetAllPostsWithIncludeAsync();
        public Task<List<Post>> GetAllPostsWithCandidateIdAsync(string candidateId);
    }
}
