using DAL.VotingSystem.Entities.UserIdentity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class AllCandidateDto
    {
        public string CandidateId { get; set; }
        public string Graduate { get; set; }
        public string Qulification { get; set; }
        public string FullName { get; set; }

    }
}
