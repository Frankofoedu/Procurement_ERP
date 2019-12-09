using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class erfxSetup1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ERFXSetup_Requisitions_Id",
                table: "ERFXSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialERFXSetup_ERFXSetup_Id",
                table: "FinancialERFXSetup");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalERFXSetup_ERFXSetup_Id",
                table: "TechnicalERFXSetup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalERFXSetup",
                table: "TechnicalERFXSetup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialERFXSetup",
                table: "FinancialERFXSetup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERFXSetup",
                table: "ERFXSetup");

            migrationBuilder.RenameTable(
                name: "TechnicalERFXSetup",
                newName: "TechnicalERFXSetups");

            migrationBuilder.RenameTable(
                name: "FinancialERFXSetup",
                newName: "FinancialERFXSetups");

            migrationBuilder.RenameTable(
                name: "ERFXSetup",
                newName: "ERFXSetups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalERFXSetups",
                table: "TechnicalERFXSetups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialERFXSetups",
                table: "FinancialERFXSetups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERFXSetups",
                table: "ERFXSetups",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 9, 15, 54, 30, 389, DateTimeKind.Local).AddTicks(4976), new DateTime(2019, 12, 9, 15, 54, 30, 389, DateTimeKind.Local).AddTicks(5837) });

            migrationBuilder.AddForeignKey(
                name: "FK_ERFXSetups_Requisitions_Id",
                table: "ERFXSetups",
                column: "Id",
                principalTable: "Requisitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialERFXSetups_ERFXSetups_Id",
                table: "FinancialERFXSetups",
                column: "Id",
                principalTable: "ERFXSetups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalERFXSetups_ERFXSetups_Id",
                table: "TechnicalERFXSetups",
                column: "Id",
                principalTable: "ERFXSetups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ERFXSetups_Requisitions_Id",
                table: "ERFXSetups");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialERFXSetups_ERFXSetups_Id",
                table: "FinancialERFXSetups");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalERFXSetups_ERFXSetups_Id",
                table: "TechnicalERFXSetups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalERFXSetups",
                table: "TechnicalERFXSetups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialERFXSetups",
                table: "FinancialERFXSetups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ERFXSetups",
                table: "ERFXSetups");

            migrationBuilder.RenameTable(
                name: "TechnicalERFXSetups",
                newName: "TechnicalERFXSetup");

            migrationBuilder.RenameTable(
                name: "FinancialERFXSetups",
                newName: "FinancialERFXSetup");

            migrationBuilder.RenameTable(
                name: "ERFXSetups",
                newName: "ERFXSetup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalERFXSetup",
                table: "TechnicalERFXSetup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialERFXSetup",
                table: "FinancialERFXSetup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ERFXSetup",
                table: "ERFXSetup",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 9, 15, 49, 1, 742, DateTimeKind.Local).AddTicks(4131), new DateTime(2019, 12, 9, 15, 49, 1, 742, DateTimeKind.Local).AddTicks(4914) });

            migrationBuilder.AddForeignKey(
                name: "FK_ERFXSetup_Requisitions_Id",
                table: "ERFXSetup",
                column: "Id",
                principalTable: "Requisitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialERFXSetup_ERFXSetup_Id",
                table: "FinancialERFXSetup",
                column: "Id",
                principalTable: "ERFXSetup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalERFXSetup_ERFXSetup_Id",
                table: "TechnicalERFXSetup",
                column: "Id",
                principalTable: "ERFXSetup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
