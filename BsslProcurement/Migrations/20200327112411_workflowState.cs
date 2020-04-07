using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class workflowState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d9e2c60-cb0d-4228-92c4-fef40475666e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30496337-d881-436f-a8ae-a6e1dc8060c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9f831e9-0ad9-4087-85c0-05a25f7c2c1a");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "WorkflowStaffs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3368141-6eea-41aa-bf71-77e440e31acf", "2bb7f160-b31e-4857-b3ab-b53918290123", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1cf0e53e-ff0b-49d4-93de-766edb567641", "66520bf7-1aef-4534-a19f-24e881e189a7", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "635a36a8-9529-4ef5-b71b-2e3684a6abef", "9f7445e2-623b-4c01-9efb-810bd9af6830", "Vendor", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cf0e53e-ff0b-49d4-93de-766edb567641");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "635a36a8-9529-4ef5-b71b-2e3684a6abef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3368141-6eea-41aa-bf71-77e440e31acf");

            migrationBuilder.DropColumn(
                name: "State",
                table: "WorkflowStaffs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9f831e9-0ad9-4087-85c0-05a25f7c2c1a", "46f00790-f13b-41e2-a7b0-47506ecf85b1", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d9e2c60-cb0d-4228-92c4-fef40475666e", "c3bab0a1-e787-4cbe-8455-e432afde5b38", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30496337-d881-436f-a8ae-a6e1dc8060c8", "12842078-8b40-438b-a525-a5fa0378cc6a", "Vendor", null });
        }
    }
}
