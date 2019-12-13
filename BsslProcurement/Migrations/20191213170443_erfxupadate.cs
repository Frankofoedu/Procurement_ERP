using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class erfxupadate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "ERFXSetups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 13, 18, 4, 42, 475, DateTimeKind.Local).AddTicks(6236), new DateTime(2019, 12, 13, 18, 4, 42, 475, DateTimeKind.Local).AddTicks(7603) });

            migrationBuilder.InsertData(
                table: "WorkflowTypes",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "0001", null, "Procurement" },
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
                name: "Submitted",
                table: "ERFXSetups");

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 9, 15, 54, 30, 389, DateTimeKind.Local).AddTicks(4976), new DateTime(2019, 12, 9, 15, 54, 30, 389, DateTimeKind.Local).AddTicks(5837) });
        }
    }
}
