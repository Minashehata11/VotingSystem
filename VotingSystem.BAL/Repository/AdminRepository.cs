using BLL.VotingSystem.Dtos;
using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using PL.VotingSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Repository
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
           _context = context;  
        }

      
        public ViewHomeAdminDto GetHomeData()
        {
            ViewHomeAdminDto data = new ViewHomeAdminDto()
            {
                NumberOfAdmin = _context.Admins.Count(),
                NumberOfCategory = _context.Categories.Count(),

            };

            return data;
        }

        public IEnumerable<ViewMangeElectionDto> MangeElectionDtos()
        {
            return
            _context.voterCandidateCategories.Include(x => x.Category).GroupBy(x => x.Category.Name).Select(x => new ViewMangeElectionDto()
            {
                CategoryName = x.Key,
                NumberOfCandidates = x.Count(),
            }) ; 
            
        }
    }
}
