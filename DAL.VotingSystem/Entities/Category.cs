using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VotingSystem.Entities
{ 
    public class Category:BaseEntity
    {
        public int CategoryId { get; set; }

        [StringLength(50),MinLength(5)]
        public string Name { get; set; }

        public byte[]? CategoryLogo { get; set; }

        public DateTime DateOfEndVoting { get; set; } = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.AddMonths(1).Month, 1);
        public List<VoterCandidateCategory> voterCandidateCategories { get; set; }

        public List<Candidate> Candidates { get; set; }

    }
}
