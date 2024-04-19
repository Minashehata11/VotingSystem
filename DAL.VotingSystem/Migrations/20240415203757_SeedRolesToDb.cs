using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.VotingSystem.Migrations
{
    public partial class SeedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
           table: "AspNetRoles",
           columns: new[] { "Id","Name", "NormalizedName", "ConcurrencyStamp" },
           values: new object[] { Guid.NewGuid().ToString(), "Voter","Voter".ToUpper(), Guid.NewGuid().ToString() }

           );

            migrationBuilder.InsertData(
              table: "AspNetRoles",
              columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
              values: new object[] { Guid.NewGuid().ToString(),"Candidate", "Candidate".ToUpper(), Guid.NewGuid().ToString() }

              );

            migrationBuilder.InsertData(
             table: "AspNetRoles",
             columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
             values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() }

             );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
