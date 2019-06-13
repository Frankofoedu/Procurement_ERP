using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class new_model_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyInfoId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "PrequalificationTaskId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "WorkflowStep",
                table: "CompanyInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Workflows",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkFlowStep",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyInfoId",
                table: "Jobs",
                column: "CompanyInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyInfoId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkFlowStep",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CompanyInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Workflows",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PrequalificationTaskId",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkflowStep",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyInfoId",
                table: "Jobs",
                column: "CompanyInfoId",
                unique: true,
                filter: "[CompanyInfoId] IS NOT NULL");
        }
    }
}
