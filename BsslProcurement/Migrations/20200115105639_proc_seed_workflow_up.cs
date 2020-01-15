using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class proc_seed_workflow_up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0620bf59-f794-4a0b-9560-b15ef54d7edf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b85ae37d-1935-4124-ada5-196ca7485dc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c20702cf-8c3f-493a-8f3c-d85f766249a5");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "c20702cf-8c3f-493a-8f3c-d85f766249a5", "cadab409-12dd-4fd5-a724-1f3b29453c3f", "Admin", null },
                    { "b85ae37d-1935-4124-ada5-196ca7485dc6", "91c3eecf-7ee2-405b-ab82-75b665fa7cce", "Staff", null },
                    { "0620bf59-f794-4a0b-9560-b15ef54d7edf", "6fe72fbc-e74a-4cfb-aead-65829858d959", "Vendor", null }
                });

            migrationBuilder.UpdateData(
                table: "Requisitions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Date", "DeliveryDate" },
                values: new object[] { new DateTime(2020, 1, 15, 11, 9, 58, 179, DateTimeKind.Local).AddTicks(1149), new DateTime(2020, 1, 15, 11, 9, 58, 179, DateTimeKind.Local).AddTicks(2235) });

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
    }
}
