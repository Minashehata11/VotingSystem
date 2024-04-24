using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Services
{
    public interface ITokenService
    {
      public Task<string> GenerateToken(ApplicationUser user); 
    }
}
