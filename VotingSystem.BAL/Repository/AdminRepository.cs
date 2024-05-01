using BLL.VotingSystem.Dtos;
using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using PL.VotingSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<List<ViewMangeElectionDto>> MangeElectionDtos()
        {
            var categories =   await  _context.Categories.Include(x => x.Candidates).ToListAsync();
            List<ViewMangeElectionDto> data=new List<ViewMangeElectionDto>();
            string base64Image="";
            foreach (var category in categories)
            {
                if(category.CategoryLogo != null)                 
                  base64Image = Convert.ToBase64String(category.CategoryLogo);

                ViewMangeElectionDto viewMangeElectionDto = new ViewMangeElectionDto
                {
                    id = category.CategoryId,
                    CategoryName = category.Name,
                    Logo = base64Image,
                    NumberOfCandidates = category.Candidates.Count(),
                };
                data.Add(viewMangeElectionDto);
            }
              return  data;
        }
            public async Task<List<Admin>> GetAllUsersWithIncludeAsync()
     => await _context.Admins.Include(v => v.User).ToListAsync();
        public async Task<Admin> GetByIdWithIncludeAsync(string id)
      => await _context.Admins.Include(v => v.User).SingleOrDefaultAsync(v => v.AdminId == id);


        public async Task<List<Admin>> SearchUserByNameAsync(string? name)
        => await _context.Admins.Include(v => v.User).Where(x => x.User.UserName.Trim().ToLower().Contains(name.Trim().ToLower())).ToListAsync();
    }
}
