using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class checkforupates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_WorkflowCategory_WorkflowCategoryId",
                table: "Workflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowCategory",
                table: "WorkflowCategory");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Workflows");

            migrationBuilder.RenameTable(
                name: "WorkflowCategory",
                newName: "WorkflowCategories");

            migrationBuilder.AddColumn<int>(
                name: "WorkflowActionId",
                table: "Workflows",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkflowTypeId",
                table: "Workflows",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkflowCategories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "WorkflowCategories",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowCategories",
                table: "WorkflowCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WorkflowActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowActions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_WorkflowActionId",
                table: "Workflows",
                column: "WorkflowActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_WorkflowActions_WorkflowActionId",
                table: "Workflows",
                column: "WorkflowActionId",
                principalTable: "WorkflowActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_WorkflowCategories_WorkflowCategoryId",
                table: "Workflows",
                column: "WorkflowCategoryId",
                principalTable: "WorkflowCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_WorkflowActions_WorkflowActionId",
                table: "Workflows");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_WorkflowCategories_WorkflowCategoryId",
                table: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkflowActions");

            migrationBuilder.DropIndex(
                name: "IX_Workflows_WorkflowActionId",
                table: "Workflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowCategories",
                table: "WorkflowCategories");

            migrationBuilder.DropColumn(
                name: "WorkflowActionId",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "WorkflowTypeId",
                table: "Workflows");

            migrationBuilder.RenameTable(
                name: "WorkflowCategories",
                newName: "WorkflowCategory");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Workflows",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkflowCategory",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "WorkflowCategory",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowCategory",
                table: "WorkflowCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_WorkflowCategory_WorkflowCategoryId",
                table: "Workflows",
                column: "WorkflowCategoryId",
                principalTable: "WorkflowCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
