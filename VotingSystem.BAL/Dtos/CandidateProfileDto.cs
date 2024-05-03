using DAL.VotingSystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class CandidateProfileDto
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string Job { get; set; }

        public string Graduate { get; set; }

        public string Qulification { get; set; }
        
    }
}
