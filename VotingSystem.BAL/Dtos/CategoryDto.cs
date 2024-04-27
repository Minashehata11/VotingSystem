using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class CategoryDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public byte[] CategoryLogo { get; set; }
        public DateTime DateOfEndVoting { get; set; }
        public List<CandidateDto> candidateDtos { get; set; }
        
    }
}