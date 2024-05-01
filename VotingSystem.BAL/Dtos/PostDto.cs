using DAL.VotingSystem.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.VotingSystem.Dtos
{
    public class PostDto
    {
        public IFormFile Post { get; set; }
        public string Decription { get; set; }
    }
}
