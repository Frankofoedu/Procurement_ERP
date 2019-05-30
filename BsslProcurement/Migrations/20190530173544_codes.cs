using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class codes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProcurementSubCategoryCode",
                table: "ProcurementSubcategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "ProcurementItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcurementCategoryCode",
                table: "ProcurementCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcurementSubCategoryCode",
                table: "ProcurementSubcategories");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "ProcurementItems");

            migrationBuilder.DropColumn(
                name: "ProcurementCategoryCode",
                table: "ProcurementCategories");
        }
    }
}
