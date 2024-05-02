using DAL.VotingSystem.Entities;
using DAL.VotingSystem.Entities.UserIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace DAL.VotingSystem.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
            .Property(p => p.Gender)
            .HasConversion<int>();

            builder.Entity<Candidate>()
            .HasOne(c => c.Category) 
            .WithMany(cat => cat.Candidates) 
            .HasForeignKey(c => c.CategoryId) 
            .OnDelete(DeleteBehavior.SetNull).IsRequired(false); 

            builder.Entity<Voter>().HasOne(v => v.Category)
                .WithOne().OnDelete(DeleteBehavior.SetNull).IsRequired(false);

            builder.Entity<VoterCandidateCategory>().HasKey(x => new { x.CategoryId, x.CandidateId,x.VoterId });
        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<VoterCandidateCategory> voterCandidateCategories { get; set; }




    }
}
