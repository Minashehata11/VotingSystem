using BLL.VotingSystem.Interfaces;
using DAL.VotingSystem.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class CreateCategoryDto
    {
        
        [StringLength(50), MinLength(5)]
        public string Name { get; set; }
        public IFormFile? CategoryLogo { get; set; }
        public DateTime DateOfEndVoting { get; set; } 
        public List<string> CandidatesId { get; set; }

    }
}
