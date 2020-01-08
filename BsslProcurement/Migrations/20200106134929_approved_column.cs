using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class approved_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4829cea6-27d5-495e-9cf9-84c7f7fe30dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f2f1b6e-cba1-4050-894f-9151b3e51ec2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6cfc094-fd62-43cd-abe6-137f0369ccc5");

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "Requisitions",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Requisitions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9f2f1b6e-cba1-4050-894f-9151b3e51ec2", "93bd72de-2e08-49bd-b25b-b22ea714043b", "Admin", null },
                    { "4829cea6-27d5-495e-9cf9-84c7f7fe30dd", "706c7fcc-8715-4bbc-b852-0fb938f5faa0", "Staff", null },
                    { "d6cfc094-fd62-43cd-abe6-137f0369ccc5", "f5edd029-2a13-45eb-b956-d165590ee9d6", "Vendor", null }
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 20, 13, 7, 48, 369, DateTimeKind.Local).AddTicks(2219), new DateTime(2019, 12, 20, 13, 7, 48, 369, DateTimeKind.Local).AddTicks(3914) });
        }
    }
}
