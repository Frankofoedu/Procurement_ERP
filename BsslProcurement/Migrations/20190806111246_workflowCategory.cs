using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflowCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Threshold",
                table: "Workflows",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "WorkflowCategoryId",
                table: "Workflows",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkflowCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_WorkflowCategoryId",
                table: "Workflows",
                column: "WorkflowCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_WorkflowCategory_WorkflowCategoryId",
                table: "Workflows",
                column: "WorkflowCategoryId",
                principalTable: "WorkflowCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_WorkflowCategory_WorkflowCategoryId",
                table: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkflowCategory");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_WorkflowCategoryId",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "Threshold",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "WorkflowCategoryId",
                table: "Workflows");
        }
    }
}
