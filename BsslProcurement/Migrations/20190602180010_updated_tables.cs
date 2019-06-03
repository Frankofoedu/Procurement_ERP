using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class updated_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterias_ProcurementItems_ProcurementItemId",
                table: "Criterias");

            migrationBuilder.DropTable(
                name: "ProcurementCriteria");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProcurementItems");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "ProcurementItems");

            migrationBuilder.RenameColumn(
                name: "ItemCode",
                table: "ProcurementItems",
                newName: "Info");

            migrationBuilder.RenameColumn(
                name: "ProcurementItemId",
                table: "Criterias",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Criterias_ProcurementItemId",
                table: "Criterias",
                newName: "IX_Criterias_ItemId");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "ProcurementItems",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CriteriaDescription",
                table: "Criterias",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemCode = table.Column<string>(nullable: true),
                    ProcurementSubcategoryId = table.Column<int>(nullable: true),
                    ItemName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ProcurementSubcategories_ProcurementSubcategoryId",
                        column: x => x.ProcurementSubcategoryId,
                        principalTable: "ProcurementSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementItems_ItemId",
                table: "ProcurementItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProcurementSubcategoryId",
                table: "Items",
                column: "ProcurementSubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criterias_Items_ItemId",
                table: "Criterias",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcurementItems_Items_ItemId",
                table: "ProcurementItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterias_Items_ItemId",
                table: "Criterias");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcurementItems_Items_ItemId",
                table: "ProcurementItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropIndex(
                name: "IX_ProcurementItems_ItemId",
                table: "ProcurementItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ProcurementItems");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "ProcurementItems",
                newName: "ItemCode");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Criterias",
                newName: "ProcurementItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Criterias_ItemId",
                table: "Criterias",
                newName: "IX_Criterias_ProcurementItemId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProcurementItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "ProcurementItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CriteriaDescription",
                table: "Criterias",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "ProcurementCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CriteriaDescription = table.Column<string>(nullable: false),
                    MinValue = table.Column<int>(nullable: true),
                    NeedsDocument = table.Column<bool>(nullable: false),
                    ProcurementItemId = table.Column<int>(nullable: true),
                    isCompulsory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcurementCriteria_ProcurementItems_ProcurementItemId",
                        column: x => x.ProcurementItemId,
                        principalTable: "ProcurementItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementCriteria_ProcurementItemId",
                table: "ProcurementCriteria",
                column: "ProcurementItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criterias_ProcurementItems_ProcurementItemId",
                table: "Criterias",
                column: "ProcurementItemId",
                principalTable: "ProcurementItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
