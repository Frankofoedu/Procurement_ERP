using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class verification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "SubmittedCriteria");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "PersonnelDetails");

            migrationBuilder.AddColumn<int>(
                name: "VerificationState",
                table: "SubmittedCriteria",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VerificationState",
                table: "PersonnelDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationState",
                table: "SubmittedCriteria");

            migrationBuilder.DropColumn(
                name: "VerificationState",
                table: "PersonnelDetails");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "SubmittedCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "PersonnelDetails",
                nullable: true);
        }
    }
}
