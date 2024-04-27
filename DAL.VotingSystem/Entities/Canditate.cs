using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.VotingSystem.Entities;
using DAL.VotingSystem.Entities.UserIdentity;

namespace DAL.VotingSystem.Entities
{
    public class Candidate:BaseEntity
    {

       
       
        [Key]
        public string CandidateId { get; set; }
        [ForeignKey("CandidateId")]
        public ApplicationUser User { get; set; }

        public string? Graduate { get; set; }

        public string? Qulification { get; set; }

        public string? Jop { get; set; }

        public int NumberOfVote { get; set; } = 0;
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public List<Post> Posts { get; set; }   // one Candidate Have Many Posts

        public List<VoterCandidateCategory> voterCandidateCategories { get; set; }
       


    }
}

