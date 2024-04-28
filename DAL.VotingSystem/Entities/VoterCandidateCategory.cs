using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VotingSystem.Entities
{
    public class VoterCandidateCategory:BaseEntity
    {
        public Voter?  Voter { get; set; }
        [ForeignKey(nameof(Voter))]
        public string VoterId { get; set; }

        public Candidate? Candidate { get; set; }
        [ForeignKey(nameof(Candidate))]
        
        public string CandidateId { get; set; }

        public Category? Category { get; set; }

        [ForeignKey(nameof(Category ))]

        public int CategoryId { get; set; }

        public DateTime DateVoter { get; set; }=DateTime.Now;
    }
}
