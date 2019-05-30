using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class itemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcurementSubcategoryId",
                table: "ProcurementItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementItems_ProcurementSubcategoryId",
                table: "ProcurementItems",
                column: "ProcurementSubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems",
                column: "ProcurementSubcategoryId",
                principalTable: "ContractSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropIndex(
                name: "IX_ProcurementItems_ProcurementSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropColumn(
                name: "ProcurementSubcategoryId",
                table: "ProcurementItems");
        }
    }
}
