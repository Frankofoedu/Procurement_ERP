using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflow_procurement_static_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cd631e9-5701-432b-8532-25f879565482");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8afbe896-67c2-4afd-83e3-ff2a27861295");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8955fdc-c893-4ac0-97fc-33022bfbfbcf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "79846dd3-bde5-4067-aff2-5fb6f1b7671c", "d846bf02-27ee-4b5a-b56a-b7c61f7e38bf", "Admin", null },
                    { "823c1e3a-3629-405f-92aa-0b841b70888c", "b6da5e4a-7a15-4a75-80d0-eaaa2aa37b03", "Staff", null },
                    { "71d76096-03a7-454c-9a6d-226c2823080d", "0983d3c8-9025-4ad4-9778-8ca0423f0811", "Vendor", null }
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2020, 1, 9, 10, 14, 39, 237, DateTimeKind.Local).AddTicks(3488), new DateTime(2020, 1, 9, 10, 14, 39, 237, DateTimeKind.Local).AddTicks(5149) });

            migrationBuilder.UpdateData(
                table: "WorkflowTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "0004");

            migrationBuilder.InsertData(
                table: "WorkflowTypes",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 4, "0001", null, "ItemPricing" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71d76096-03a7-454c-9a6d-226c2823080d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79846dd3-bde5-4067-aff2-5fb6f1b7671c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "823c1e3a-3629-405f-92aa-0b841b70888c");

            migrationBuilder.DeleteData(
                table: "WorkflowTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cd631e9-5701-432b-8532-25f879565482", "d6f33543-ede9-4a57-a9f0-18ac4566db64", "Admin", null },
                    { "b8955fdc-c893-4ac0-97fc-33022bfbfbcf", "0c3b14ee-56be-453b-bf40-2625693cd220", "Staff", null },
                    { "8afbe896-67c2-4afd-83e3-ff2a27861295", "dc7f98b2-8c9a-46d2-9ab0-6583cbcae710", "Vendor", null }
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2020, 1, 6, 14, 49, 28, 247, DateTimeKind.Local).AddTicks(7362), new DateTime(2020, 1, 6, 14, 49, 28, 247, DateTimeKind.Local).AddTicks(8476) });

            migrationBuilder.UpdateData(
                table: "WorkflowTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Code",
                value: "0001");
        }
    }
}
