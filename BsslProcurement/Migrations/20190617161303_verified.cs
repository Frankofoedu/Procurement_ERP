using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class verified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "SubmittedCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "PersonnelDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "SubmittedCriteria");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "PersonnelDetails");
        }
    }
}
