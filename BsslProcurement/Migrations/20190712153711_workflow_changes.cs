using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflow_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlternativeStaffId",
                table: "Workflows",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_AlternativeStaffId",
                table: "Workflows",
                column: "AlternativeStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_AspNetUsers_AlternativeStaffId",
                table: "Workflows",
                column: "AlternativeStaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_AspNetUsers_AlternativeStaffId",
                table: "Workflows");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_AlternativeStaffId",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "AlternativeStaffId",
                table: "Workflows");
        }
    }
}
