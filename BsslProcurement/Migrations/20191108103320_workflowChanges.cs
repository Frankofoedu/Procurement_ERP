using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflowChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_AspNetUsers_AlternativeStaffId",
                table: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkflowCategoryActionStaffs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "Threshold",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "ToPersonOrAssign",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "WorkflowCategoryId",
                table: "Workflows");

            migrationBuilder.RenameColumn(
                name: "AlternativeStaffId",
                table: "Workflows",
                newName: "StaffId1");

            migrationBuilder.RenameIndex(
                name: "IX_Workflows_AlternativeStaffId",
                table: "Workflows",
                newName: "IX_Workflows_StaffId1");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkflowActions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERFx",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessType",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcurementMethod",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkflowStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkflowId = table.Column<int>(nullable: false),
                    StaffId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowStaffs_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowStaffs_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 11, 8, 11, 33, 19, 148, DateTimeKind.Local).AddTicks(4272), new DateTime(2019, 11, 8, 11, 33, 19, 184, DateTimeKind.Local).AddTicks(7517) });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStaffs_StaffId",
                table: "WorkflowStaffs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStaffs_WorkflowId",
                table: "WorkflowStaffs",
                column: "WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_AspNetUsers_StaffId1",
                table: "Workflows",
                column: "StaffId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_AspNetUsers_StaffId1",
                table: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkflowStaffs");

            migrationBuilder.DropColumn(
                name: "ERFx",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "ProcessType",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "ProcurementMethod",
                table: "Requisitions");

            migrationBuilder.RenameColumn(
                name: "StaffId1",
                table: "Workflows",
                newName: "AlternativeStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Workflows_StaffId1",
                table: "Workflows",
                newName: "IX_Workflows_AlternativeStaffId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Workflows",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Threshold",
                table: "Workflows",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "ToPersonOrAssign",
                table: "Workflows",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WorkflowCategoryId",
                table: "Workflows",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkflowActions",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "WorkflowCategoryActionStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<string>(nullable: true),
                    WorkflowActionId = table.Column<int>(nullable: false),
                    WorkflowTypeId = table.Column<int>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 11, 7, 13, 10, 25, 793, DateTimeKind.Local).AddTicks(4600), new DateTime(2019, 11, 7, 13, 10, 25, 794, DateTimeKind.Local).AddTicks(5153) });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_AspNetUsers_AlternativeStaffId",
                table: "Workflows",
                column: "AlternativeStaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
