using BLL.VotingSystem.Dtos;
using DAL.VotingSystem.Entities;
using PL.VotingSystem.Dtos;

namespace BLL.VotingSystem.Interfaces
{
    public interface IAdminRepository:IGenericRepository<Admin>
    {
        public ViewHomeAdminDto GetHomeData();
        public Task<List<ViewMangeElectionDto>> MangeElectionDtos();
        public Task<List<Admin>> GetAllUsersWithIncludeAsync();
        public Task<List<Admin>> SearchUserByNameAsync(string? name);
        public Task<Admin> GetByIdWithIncludeAsync(string id);

    }
}
