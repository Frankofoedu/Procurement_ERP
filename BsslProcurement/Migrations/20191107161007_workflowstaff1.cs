using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflowstaff1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowCategoryActionStaffs_WorkflowTypes_WorkflowTypeId",
                table: "WorkflowCategoryActionStaffs");

            migrationBuilder.DropColumn(
                name: "WorkflowCategoryId",
                table: "WorkflowCategoryActionStaffs");

            migrationBuilder.AlterColumn<int>(
                name: "WorkflowTypeId",
                table: "WorkflowCategoryActionStaffs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowCategoryActionStaffs_WorkflowTypes_WorkflowTypeId",
                table: "WorkflowCategoryActionStaffs",
                column: "WorkflowTypeId",
                principalTable: "WorkflowTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowCategoryActionStaffs_WorkflowTypes_WorkflowTypeId",
                table: "WorkflowCategoryActionStaffs");

            migrationBuilder.AlterColumn<int>(
                name: "WorkflowTypeId",
                table: "WorkflowCategoryActionStaffs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "WorkflowCategoryId",
                table: "WorkflowCategoryActionStaffs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowCategoryActionStaffs_WorkflowTypes_WorkflowTypeId",
                table: "WorkflowCategoryActionStaffs",
                column: "WorkflowTypeId",
                principalTable: "WorkflowTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
