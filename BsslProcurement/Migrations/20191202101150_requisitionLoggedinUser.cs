using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class requisitionLoggedinUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoggedInUserCode",
                table: "Requisitions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequisitionId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 2, 11, 11, 48, 624, DateTimeKind.Local).AddTicks(8832), new DateTime(2019, 12, 2, 11, 11, 48, 624, DateTimeKind.Local).AddTicks(9968) });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RequisitionId",
                table: "Jobs",
                column: "RequisitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Requisitions_RequisitionId",
                table: "Jobs",
                column: "RequisitionId",
                principalTable: "Requisitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Requisitions_RequisitionId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_RequisitionId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "LoggedInUserCode",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "RequisitionId",
                table: "Jobs");

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 11, 27, 9, 9, 52, 147, DateTimeKind.Local).AddTicks(69), new DateTime(2019, 11, 27, 9, 9, 52, 148, DateTimeKind.Local).AddTicks(5096) });
        }
    }
}
