using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflowcategory2workflowtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_WorkflowCategories_WorkflowCategoryId",
                table: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkflowCategories");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_WorkflowCategoryId",
                table: "Workflows");

            migrationBuilder.AddColumn<int>(
                name: "WorkflowTypeId",
                table: "Workflows",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkflowTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_WorkflowTypeId",
                table: "Workflows",
                column: "WorkflowTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_WorkflowTypes_WorkflowTypeId",
                table: "Workflows",
                column: "WorkflowTypeId",
                principalTable: "WorkflowTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_WorkflowTypes_WorkflowTypeId",
                table: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkflowTypes");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_WorkflowTypeId",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "WorkflowTypeId",
                table: "Workflows");

            migrationBuilder.CreateTable(
                name: "WorkflowCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_WorkflowCategoryId",
                table: "Workflows",
                column: "WorkflowCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_WorkflowCategories_WorkflowCategoryId",
                table: "Workflows",
                column: "WorkflowCategoryId",
                principalTable: "WorkflowCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
