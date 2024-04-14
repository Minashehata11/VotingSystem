using BLL.VotingSystem.Dtos;
using DAL.VotingSystem.Entities;
using PL.VotingSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface IAdminRepository:IGenericRepository<Admin>
    {
        public ViewHomeAdminDto GetHomeData();
        public IEnumerable<ViewMangeElectionDto> MangeElectionDtos();
    }
}
