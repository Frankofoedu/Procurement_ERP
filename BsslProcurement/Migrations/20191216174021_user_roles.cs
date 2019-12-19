using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class user_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f71e2b41-37cb-448e-94cf-164c311d0320", "68ed45c2-0a3b-4586-ae73-5c02d1dcd38c", "Admin", null },
                    { "e50f214c-7959-495c-a5b4-5657350a1bc8", "fbec9841-1ace-4bd4-9f01-96b88f59af34", "Staff", null },
                    { "d660824e-d429-484b-b46d-0615059b26f6", "8b979bfa-9ebc-4bb7-b727-d0268e491afc", "Vendor", null }
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 16, 18, 40, 20, 378, DateTimeKind.Local).AddTicks(5968), new DateTime(2019, 12, 16, 18, 40, 20, 378, DateTimeKind.Local).AddTicks(7176) });

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d660824e-d429-484b-b46d-0615059b26f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e50f214c-7959-495c-a5b4-5657350a1bc8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f71e2b41-37cb-448e-94cf-164c311d0320");

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

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 9, 15, 54, 30, 389, DateTimeKind.Local).AddTicks(4976), new DateTime(2019, 12, 9, 15, 54, 30, 389, DateTimeKind.Local).AddTicks(5837) });
        }
    }
}
