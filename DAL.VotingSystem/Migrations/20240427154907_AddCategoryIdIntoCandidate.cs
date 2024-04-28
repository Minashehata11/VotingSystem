using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    public partial class AddCategoryIdIntoCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CategoryId",
                table: "Candidates",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId"
                 );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CategoryId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Candidates");
        }
    }
}
