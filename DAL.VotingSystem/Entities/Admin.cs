using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VotingSystem.Entities
{ 
    
     
    public class Admin:BaseEntity
    {
      
        [Key]
        public string AdminId { get; set; }
        [ForeignKey("AdminId")]
        public  ApplicationUser User { get; set; }

    }
       

    }


