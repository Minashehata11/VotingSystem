using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class ViewMangeElectionDto
    {
        public int id { get; set; }
        public string CategoryName { get; set; }

        public int NumberOfCandidates { get; set;}

        public string Logo { get; set; }
    }
}
