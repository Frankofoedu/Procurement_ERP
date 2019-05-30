using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class itemUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ProcurementGroups_ProcurementGroupId",
                table: "ProcurementItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProcurementGroupId",
                table: "ProcurementItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProcurementSubcategoryId",
                table: "ProcurementItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems",
                column: "ProcurementSubcategoryId",
                principalTable: "ContractSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ProcurementGroups_ProcurementGroupId",
                table: "ProcurementItems",
                column: "ProcurementGroupId",
                principalTable: "ProcurementGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ProcurementGroups_ProcurementGroupId",
                table: "ProcurementItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProcurementGroupId",
                table: "ProcurementItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProcurementSubcategoryId",
                table: "ProcurementItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems",
                column: "ProcurementSubcategoryId",
                principalTable: "ContractSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ProcurementGroups_ProcurementGroupId",
                table: "ProcurementItems",
                column: "ProcurementGroupId",
                principalTable: "ProcurementGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
