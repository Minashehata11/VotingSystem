using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    public partial class EditVotingTableTo3CompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_voterCandidateCategories",
                table: "voterCandidateCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_voterCandidateCategories",
                table: "voterCandidateCategories",
                columns: new[] { "CategoryId", "CandidateId", "VoterId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_voterCandidateCategories",
                table: "voterCandidateCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_voterCandidateCategories",
                table: "voterCandidateCategories",
                columns: new[] { "CategoryId", "CandidateId" });
        }
    }
}
