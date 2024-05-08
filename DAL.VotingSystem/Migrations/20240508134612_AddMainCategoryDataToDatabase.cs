using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    public partial class AddMainCategoryDataToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string hexString = "0x89504E470D0A1A0A0000000D49484452000007D0000005350802000000C44CC8360000000467414D410000B18F0BFC6105000000206348524D00007A26000080840000FA00000080E8000075300000EA6000003A98000017709CBA513C00000006624B474400FF00FF00FFA0BDA793000080004944415478DAECDD779C5C6779"; // Replace ... with your actual logo data
            byte[] imageBytes = Encoding.ASCII.GetBytes(hexString.Substring(2));
            migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Name", "CategoryLogo", "DateOfEndVoting" },
            values: new object[] { "Presidential election", imageBytes, DateTime.Parse("14/05/2025 12:00:00 ص") }

            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Name",
            keyValue: "Presidential election"
        );

        }
    }
}
