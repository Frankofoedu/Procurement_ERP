using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "ProcurementItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProcurementItems",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isCompulsory",
                table: "ProcurementCriteria",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "NeedsDocument",
                table: "ProcurementCriteria",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CriteriaDescription",
                table: "ProcurementCriteria",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProcurementItems");

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "ProcurementItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<bool>(
                name: "isCompulsory",
                table: "ProcurementCriteria",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "NeedsDocument",
                table: "ProcurementCriteria",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "CriteriaDescription",
                table: "ProcurementCriteria",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
