﻿// <auto-generated />
using System;
using DAL.VotingSystem.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240508134612_AddMainCategoryDataToDatabase")]
    partial class AddMainCategoryDataToDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.VotingSystem.Entities.Admin", b =>
                {
                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Candidate", b =>
                {
                    b.Property<string>("CandidateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Graduate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfVote")
                        .HasColumnType("int");

                    b.Property<string>("Qulification")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CandidateId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<byte[]>("CategoryLogo")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("DateOfEndVoting")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<string>("CandidateId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PostImage")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("PostId");

                    b.HasIndex("CandidateId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SSN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Voter", b =>
                {
                    b.Property<string>("VoterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<byte[]>("ImageCard")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("VoterId");

                    b.HasIndex("CategoryId")
                        .IsUnique()
                        .HasFilter("[CategoryId] IS NOT NULL");

                    b.ToTable("Voters");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.VoterCandidateCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CandidateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VoterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateVoter")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId", "CandidateId", "VoterId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("VoterId");

                    b.ToTable("voterCandidateCategories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<string>", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Admin", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", "User")
                        .WithOne("Admin")
                        .HasForeignKey("DAL.VotingSystem.Entities.Admin", "AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Candidate", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", "User")
                        .WithOne("Candidate")
                        .HasForeignKey("DAL.VotingSystem.Entities.Candidate", "CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.VotingSystem.Entities.Category", "Category")
                        .WithMany("Candidates")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Post", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.Candidate", "Candidate")
                        .WithMany("Posts")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Voter", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.Category", "Category")
                        .WithOne()
                        .HasForeignKey("DAL.VotingSystem.Entities.Voter", "CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", "User")
                        .WithOne("Voter")
                        .HasForeignKey("DAL.VotingSystem.Entities.Voter", "VoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.VoterCandidateCategory", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.Candidate", "Candidate")
                        .WithMany("voterCandidateCategories")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.VotingSystem.Entities.Category", "Category")
                        .WithMany("voterCandidateCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.VotingSystem.Entities.Voter", "Voter")
                        .WithMany("voterCandidateCategories")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Category");

                    b.Navigation("Voter");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Candidate", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("voterCandidateCategories");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Category", b =>
                {
                    b.Navigation("Candidates");

                    b.Navigation("voterCandidateCategories");
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.UserIdentity.ApplicationUser", b =>
                {
                    b.Navigation("Admin")
                        .IsRequired();

                    b.Navigation("Candidate")
                        .IsRequired();

                    b.Navigation("Voter")
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.VotingSystem.Entities.Voter", b =>
                {
                    b.Navigation("voterCandidateCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
