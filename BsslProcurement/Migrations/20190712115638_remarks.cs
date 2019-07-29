using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class remarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ProcurementSubcategories_ProcurementSubcategoryId",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProcurementSubcategoryId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "Items",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ProcurementSubcategories_ProcurementSubcategoryId",
                table: "Items",
                column: "ProcurementSubcategoryId",
                principalTable: "ProcurementSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ProcurementSubcategories_ProcurementSubcategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "ProcurementSubcategoryId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                table: "Items",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ProcurementSubcategories_ProcurementSubcategoryId",
                table: "Items",
                column: "ProcurementSubcategoryId",
                principalTable: "ProcurementSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
