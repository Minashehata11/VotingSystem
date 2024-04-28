using DAL.VotingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class CandidateProfileDto
    {
        public byte[]? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Birthday { get; set; }
        public Enum Gender { get; set; }
        public string Job { get; set; }

        public string Graduate { get; set; }

        public string Qulification { get; set; }
        public List<Post> Posts { get; set; }   // one Candidate Have Many Posts
        
    }
}
