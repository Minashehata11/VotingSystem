using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VotingSystem.Entities
{ 
    public class Category
    {
        public int? CategoryId { get; set; }

        [StringLength(50),MinLength(5)]
        public string Name { get; set; }

        public List<VoterCandidateCategory> voterCandidateCategories { get; set; }

    }
}
