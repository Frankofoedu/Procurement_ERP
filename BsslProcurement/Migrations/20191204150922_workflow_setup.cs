using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflow_setup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedInUserCode",
                table: "Requisitions");

            migrationBuilder.AddColumn<string>(
                name: "LoggedInUserId",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 4, 16, 9, 20, 428, DateTimeKind.Local).AddTicks(1701), new DateTime(2019, 12, 4, 16, 9, 20, 428, DateTimeKind.Local).AddTicks(3344) });

            migrationBuilder.InsertData(
                table: "WorkflowTypes",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "0001", null, "ItemPricing" },
                    { 2, "0002", null, "Prequalification" },
                    { 3, "0003", null, "Requisition" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkflowTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkflowTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkflowTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "LoggedInUserId",
                table: "Requisitions");

            migrationBuilder.AddColumn<string>(
                name: "LoggedInUserCode",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 2, 11, 11, 48, 624, DateTimeKind.Local).AddTicks(8832), new DateTime(2019, 12, 2, 11, 11, 48, 624, DateTimeKind.Local).AddTicks(9968) });
        }
    }
}
