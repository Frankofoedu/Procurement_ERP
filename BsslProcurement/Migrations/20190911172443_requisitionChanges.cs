using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class requisitionChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestionDepartment",
                table: "Requisitions",
                newName: "RequesterValue");

            migrationBuilder.AddColumn<string>(
                name: "RequesterType",
                table: "Requisitions",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequesterType",
                table: "Requisitions");

            migrationBuilder.RenameColumn(
                name: "RequesterValue",
                table: "Requisitions",
                newName: "RequestionDepartment");
        }
    }
}
