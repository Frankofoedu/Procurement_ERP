using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class requisitionUpdate1 : Migration
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

            migrationBuilder.AddColumn<bool>(
                name: "isBudgetCleared",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 4, 9, 50, 38, 812, DateTimeKind.Local).AddTicks(8219), new DateTime(2019, 12, 4, 9, 50, 38, 812, DateTimeKind.Local).AddTicks(9076) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedInUserId",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "isBudgetCleared",
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
