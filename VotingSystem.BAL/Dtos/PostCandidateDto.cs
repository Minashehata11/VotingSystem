using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class PostCandidateDto
    {
        public int PostId { get; set; }
        public string PostImage { get; set; }
        public string Description { get; set; }
    }
}
