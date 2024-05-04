using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.VotingSystem.Entities.UserIdentity
{

    public enum Gender

    {
        Male,
        Female
    }

    public class ApplicationUser:IdentityUser
    {
        public Admin Admin { get; set; }
        public Candidate Candidate { get; set; }
        public Voter Voter { get; set; }
        public string SSN { get; set; }
        public Gender Gender { get; set; }

        [StringLength(20), MinLength(3)]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string City { get; set; }
        [MaxLength(40)]
        public string? Street { get; set; }
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public int Age { get; set; }
        public byte[]? Image { get; set; }


    }
}
