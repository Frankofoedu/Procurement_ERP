using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class new_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractSubcategories_ContractCategories_ProcurementCategoryId",
                table: "ContractSubcategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractSubcategories",
                table: "ContractSubcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractCategories",
                table: "ContractCategories");

            migrationBuilder.RenameTable(
                name: "ContractSubcategories",
                newName: "ProcurementSubcategories");

            migrationBuilder.RenameTable(
                name: "ContractCategories",
                newName: "ProcurementCategories");

            migrationBuilder.RenameColumn(
                name: "SubcategoryName",
                table: "ProcurementSubcategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SubcategoryDescription",
                table: "ProcurementSubcategories",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_ContractSubcategories_ProcurementCategoryId",
                table: "ProcurementSubcategories",
                newName: "IX_ProcurementSubcategories_ProcurementCategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "ProcurementCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryDescription",
                table: "ProcurementCategories",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "ProcurementGroups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcurementContractCategoryId",
                table: "ProcurementSubcategories",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcurementSubcategories",
                table: "ProcurementSubcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcurementCategories",
                table: "ProcurementCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Criterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CriteriaDescription = table.Column<string>(nullable: true),
                    MinValue = table.Column<int>(nullable: true),
                    isCompulsory = table.Column<bool>(nullable: false),
                    NeedsDocument = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ProcurementCategoryId = table.Column<int>(nullable: true),
                    ProcurementItemId = table.Column<int>(nullable: true),
                    ProcurementSubcategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterias_ProcurementCategories_ProcurementCategoryId",
                        column: x => x.ProcurementCategoryId,
                        principalTable: "ProcurementCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Criterias_ProcurementItems_ProcurementItemId",
                        column: x => x.ProcurementItemId,
                        principalTable: "ProcurementItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Criterias_ProcurementSubcategories_ProcurementSubcategoryId",
                        column: x => x.ProcurementSubcategoryId,
                        principalTable: "ProcurementSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_ProcurementCategoryId",
                table: "Criterias",
                column: "ProcurementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_ProcurementItemId",
                table: "Criterias",
                column: "ProcurementItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_ProcurementSubcategoryId",
                table: "Criterias",
                column: "ProcurementSubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ProcurementSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems",
                column: "ProcurementSubcategoryId",
                principalTable: "ProcurementSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementSubcategories_ProcurementCategories_ProcurementCategoryId",
                table: "ProcurementSubcategories",
                column: "ProcurementCategoryId",
                principalTable: "ProcurementCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_ProcurementSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementSubcategories_ProcurementCategories_ProcurementCategoryId",
                table: "ProcurementSubcategories");

            migrationBuilder.DropTable(
                name: "Criterias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcurementSubcategories",
                table: "ProcurementSubcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcurementCategories",
                table: "ProcurementCategories");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "ProcurementGroups");

            migrationBuilder.DropColumn(
                name: "ProcurementContractCategoryId",
                table: "ProcurementSubcategories");

            migrationBuilder.RenameTable(
                name: "ProcurementSubcategories",
                newName: "ContractSubcategories");

            migrationBuilder.RenameTable(
                name: "ProcurementCategories",
                newName: "ContractCategories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContractSubcategories",
                newName: "SubcategoryName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ContractSubcategories",
                newName: "SubcategoryDescription");

            migrationBuilder.RenameIndex(
                name: "IX_ProcurementSubcategories_ProcurementCategoryId",
                table: "ContractSubcategories",
                newName: "IX_ContractSubcategories_ProcurementCategoryId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContractCategories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ContractCategories",
                newName: "CategoryDescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractSubcategories",
                table: "ContractSubcategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractCategories",
                table: "ContractCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractSubcategories_ContractCategories_ProcurementCategoryId",
                table: "ContractSubcategories",
                column: "ProcurementCategoryId",
                principalTable: "ContractCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_ContractSubcategories_ProcurementSubcategoryId",
                table: "ProcurementItems",
                column: "ProcurementSubcategoryId",
                principalTable: "ContractSubcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
