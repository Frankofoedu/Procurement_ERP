using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: false),
                    CategoryDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupName = table.Column<string>(nullable: true),
                    NoOfCategory = table.Column<int>(nullable: false),
                    OpeningDate = table.Column<DateTime>(nullable: true),
                    ClosingDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementPortalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PortalName = table.Column<string>(nullable: true),
                    PortalLogo = table.Column<string>(nullable: true),
                    Legal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementPortalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractSubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractCategoryId = table.Column<int>(nullable: true),
                    SubcategoryName = table.Column<string>(nullable: false),
                    SubcategoryDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractSubcategories_ContractCategories_ContractCategoryId",
                        column: x => x.ContractCategoryId,
                        principalTable: "ContractCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProcurementGroupId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcurementItems_ProcurementGroups_ProcurementGroupId",
                        column: x => x.ProcurementGroupId,
                        principalTable: "ProcurementGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProcurementItemId = table.Column<int>(nullable: true),
                    CriteriaDescription = table.Column<string>(nullable: true),
                    MinValue = table.Column<int>(nullable: true),
                    isCompulsory = table.Column<bool>(nullable: true),
                    NeedsDocument = table.Column<bool>(nullable: true)
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
                name: "IX_ContractSubcategories_ContractCategoryId",
                table: "ContractSubcategories",
                column: "ContractCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementCriteria_ProcurementItemId",
                table: "ProcurementCriteria",
                column: "ProcurementItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementItems_ProcurementGroupId",
                table: "ProcurementItems",
                column: "ProcurementGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractSubcategories");

            migrationBuilder.DropTable(
                name: "ProcurementCriteria");

            migrationBuilder.DropTable(
                name: "ProcurementPortalInfo");

            migrationBuilder.DropTable(
                name: "ContractCategories");

            migrationBuilder.DropTable(
                name: "ProcurementItems");

            migrationBuilder.DropTable(
                name: "ProcurementGroups");
        }
    }
}
