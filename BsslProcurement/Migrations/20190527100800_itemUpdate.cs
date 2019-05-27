using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class itemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractSubcategoryId",
                table: "ProcurementItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementItems_ContractSubcategoryId",
                table: "ProcurementItems",
                column: "ContractSubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ContractSubcategoryId",
                table: "ProcurementItems",
                column: "ContractSubcategoryId",
                principalTable: "ContractSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ContractSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropIndex(
                name: "IX_ProcurementItems_ContractSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropColumn(
                name: "ContractSubcategoryId",
                table: "ProcurementItems");
        }
    }
}
