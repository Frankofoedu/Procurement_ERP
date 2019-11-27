using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffId1",
                table: "Jobs",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 11, 27, 9, 9, 52, 147, DateTimeKind.Local).AddTicks(69), new DateTime(2019, 11, 27, 9, 9, 52, 148, DateTimeKind.Local).AddTicks(5096) });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_StaffId1",
                table: "Jobs",
                column: "StaffId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_StaffId1",
                table: "Jobs",
                column: "StaffId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_StaffId1",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_StaffId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "StaffId1",
                table: "Jobs");

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 11, 13, 10, 17, 36, 870, DateTimeKind.Local).AddTicks(633), new DateTime(2019, 11, 13, 10, 17, 36, 871, DateTimeKind.Local).AddTicks(355) });
        }
    }
}
