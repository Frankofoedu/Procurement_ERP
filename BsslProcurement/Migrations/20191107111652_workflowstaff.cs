using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflowstaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkflowCategoryActionStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkflowCategoryId = table.Column<int>(nullable: false),
                    WorkflowActionId = table.Column<int>(nullable: false),
                    StaffId = table.Column<string>(nullable: true),
                    WorkflowTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowCategoryActionStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowCategoryActionStaffs_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowCategoryActionStaffs_WorkflowActions_WorkflowActionId",
                        column: x => x.WorkflowActionId,
                        principalTable: "WorkflowActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkflowCategoryActionStaffs_WorkflowTypes_WorkflowTypeId",
                        column: x => x.WorkflowTypeId,
                        principalTable: "WorkflowTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowCategoryActionStaffs_StaffId",
                table: "WorkflowCategoryActionStaffs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowCategoryActionStaffs_WorkflowActionId",
                table: "WorkflowCategoryActionStaffs",
                column: "WorkflowActionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowCategoryActionStaffs_WorkflowTypeId",
                table: "WorkflowCategoryActionStaffs",
                column: "WorkflowTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowCategoryActionStaffs");
        }
    }
}
