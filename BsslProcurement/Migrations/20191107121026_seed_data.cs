using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class seed_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Requisitions",
                columns: new[] { "Id", "Date", "DeliveryDate", "Description", "PRNumber", "PreparedBy", "PreparedByRank", "PreparedFor", "PreparedForRank", "ProcurementType", "Purpose", "RequesterType", "RequesterValue", "RequiredAtDepartment", "isPriced", "isSubmitted" },
                values: new object[] { 22, new DateTime(2019, 11, 7, 13, 10, 25, 793, DateTimeKind.Local).AddTicks(4600), new DateTime(2019, 11, 7, 13, 10, 25, 794, DateTimeKind.Local).AddTicks(5153), "sample requisition", "000222", "John O", "HOD", "Abbah", "Hod", null, "For general stores", "Division", "kkkkkd", "head office", false, true });

            migrationBuilder.InsertData(
                table: "RequisitionItems",
                columns: new[] { "Id", "AttachmentId", "Description", "Quantity", "RequisitionId", "StoreItemCode", "UnitOfMeasurement", "UnitPrice", "VendorId" },
                values: new object[] { 12, null, "biro", 22, 22, "22", null, 0.0, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RequisitionItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22);
        }
    }
}
