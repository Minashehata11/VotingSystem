using BLL.VotingSystem.Dtos;
using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Interfaces
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        public Task<CategoryDto> GetByIdWithIncludeAsync(int id);
        public Task<Category> GetById(int id);
    }
}
