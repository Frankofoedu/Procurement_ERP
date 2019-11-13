using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class requisitionUpdateDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 11, 13, 10, 17, 36, 870, DateTimeKind.Local).AddTicks(633), new DateTime(2019, 11, 13, 10, 17, 36, 871, DateTimeKind.Local).AddTicks(355) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 11, 13, 10, 8, 10, 20, DateTimeKind.Local).AddTicks(760), new DateTime(2019, 11, 13, 10, 8, 10, 21, DateTimeKind.Local).AddTicks(1595) });
        }
    }
}
