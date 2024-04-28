using DAL.VotingSystem.Entities;
using DAL.VotingSystem.Entities.UserIdentity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.VotingSystem.Entities
{
    public class Voter:BaseEntity
    {
       
        [Key]
        public string VoterId { get; set; }
        [ForeignKey("VoterId")]
        public ApplicationUser User { get; set; }
        public byte[]?  ImageCard { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public List<VoterCandidateCategory> voterCandidateCategories { get; set; }
    }
}
