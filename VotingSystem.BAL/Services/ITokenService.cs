using DAL.VotingSystem.Entities.UserIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Services
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user); 
    }
}
