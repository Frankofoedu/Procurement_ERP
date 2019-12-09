using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class erfxSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ERFXSetup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ErfxDate = table.Column<DateTime>(nullable: true),
                    ProjectTitle = table.Column<string>(nullable: true),
                    BidType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERFXSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ERFXSetup_Requisitions_Id",
                        column: x => x.Id,
                        principalTable: "Requisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialERFXSetup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BidStartDate = table.Column<DateTime>(nullable: true),
                    BidEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialERFXSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialERFXSetup_ERFXSetup_Id",
                        column: x => x.Id,
                        principalTable: "ERFXSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalERFXSetup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BidStartDate = table.Column<DateTime>(nullable: true),
                    BidEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalERFXSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalERFXSetup_ERFXSetup_Id",
                        column: x => x.Id,
                        principalTable: "ERFXSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 9, 15, 49, 1, 742, DateTimeKind.Local).AddTicks(4131), new DateTime(2019, 12, 9, 15, 49, 1, 742, DateTimeKind.Local).AddTicks(4914) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialERFXSetup");

            migrationBuilder.DropTable(
                name: "TechnicalERFXSetup");

            migrationBuilder.DropTable(
                name: "ERFXSetup");

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2019, 12, 4, 9, 50, 38, 812, DateTimeKind.Local).AddTicks(8219), new DateTime(2019, 12, 4, 9, 50, 38, 812, DateTimeKind.Local).AddTicks(9076) });
        }
    }
}
