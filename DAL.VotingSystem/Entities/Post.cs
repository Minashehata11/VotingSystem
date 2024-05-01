using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VotingSystem.Entities
{
    public class Post:BaseEntity
    {
        public int PostId { get; set; }

        public byte[]? PostImage { get; set; }

        public string Decription { get; set; }

        public Candidate? Candidate { get; set; }

        [ForeignKey(nameof(Candidate))]
        public string CandidateId { get; set; }
    }
}
