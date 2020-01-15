using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class proc_seed_workflow_new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57f2df19-a765-46b1-bcde-231bd2b66766");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1749784-db28-4a84-a959-7edeee1f8eed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb71e2e0-99ac-4986-9442-7442d40cbacb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5404845e-ce0e-42a7-8abf-eecef93bffaf", "355be413-5b8b-49d4-a042-d99815dff19a", "Admin", null },
                    { "c197e12b-fba6-41c6-aa80-0e3aff5aa4f4", "98afc93b-585c-4b67-a3fc-fb24d54172e2", "Staff", null },
                    { "08604223-9063-4608-a0be-31a823ac6472", "1fe2e332-8293-4f0f-bef5-76c4e1b250b8", "Vendor", null }
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2020, 1, 15, 12, 3, 56, 787, DateTimeKind.Local).AddTicks(2560), new DateTime(2020, 1, 15, 12, 3, 56, 787, DateTimeKind.Local).AddTicks(3905) });

            migrationBuilder.InsertData(
                table: "Workflows",
                columns: new[] { "Id", "Step", "WorkflowActionId", "WorkflowTypeId" },
                values: new object[,]
                {
                    { 100, 1, 100, 3 },
                    { 200, 2, 200, 3 },
                    { 300, 3, 300, 3 },
                    { 400, 4, 400, 3 },
                    { 500, 5, 500, 3 },
                    { 600, 6, 600, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08604223-9063-4608-a0be-31a823ac6472");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5404845e-ce0e-42a7-8abf-eecef93bffaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c197e12b-fba6-41c6-aa80-0e3aff5aa4f4");

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "Workflows",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b1749784-db28-4a84-a959-7edeee1f8eed", "cc8bbd0a-3a87-4393-852f-6c6f04c3b118", "Admin", null },
                    { "cb71e2e0-99ac-4986-9442-7442d40cbacb", "fc5aacf7-291b-474f-a25b-ce015e9c06a0", "Staff", null },
                    { "57f2df19-a765-46b1-bcde-231bd2b66766", "b60878d1-e777-4463-8022-420c16b9d572", "Vendor", null }
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2020, 1, 15, 11, 56, 37, 457, DateTimeKind.Local).AddTicks(3043), new DateTime(2020, 1, 15, 11, 56, 37, 457, DateTimeKind.Local).AddTicks(4714) });
        }
    }
}
