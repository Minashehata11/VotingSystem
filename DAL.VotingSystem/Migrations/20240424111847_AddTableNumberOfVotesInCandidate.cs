using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    public partial class AddTableNumberOfVotesInCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVots",
                table: "Voters");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVote",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVote",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVots",
                table: "Voters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
