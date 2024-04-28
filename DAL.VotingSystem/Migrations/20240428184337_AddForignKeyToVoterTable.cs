using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    public partial class AddForignKeyToVoterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Voters",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Candidates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Voters_CategoryId",
                table: "Voters",
                column: "CategoryId",
                unique: true,
                filter: "[CategoryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_Categories_CategoryId",
                table: "Voters",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voters_Categories_CategoryId",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Voters_CategoryId",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Voters");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
