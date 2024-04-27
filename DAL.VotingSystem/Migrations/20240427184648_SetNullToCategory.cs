using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    public partial class SetNullToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Candidates",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Candidates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
