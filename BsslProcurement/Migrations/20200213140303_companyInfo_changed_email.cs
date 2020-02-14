using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class companyInfo_changed_email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fb48a5c-c35d-4d2a-876e-69d1d0e4bd73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae749f49-41f2-43ce-a863-9952af9fcc1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e64c769f-eb7a-4e9b-949d-a2a4e0360181");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CompanyInfo");

            migrationBuilder.AddColumn<string>(
                name: "CompanyEmail",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c70bd76-6e55-41cc-b4d9-ac3f39674d25", "6ca82a43-3539-4d87-b484-6623463dcb90", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8fab1f38-5bca-4a50-80c4-502fbe2a932e", "6679fa9c-c6b1-4c3b-8d79-896b9f62a6cc", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd7026d1-41d6-42a8-9ef1-a979bd3dbc29", "89c902e7-ece7-4d08-9e7a-94147b58a8d5", "Vendor", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c70bd76-6e55-41cc-b4d9-ac3f39674d25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fab1f38-5bca-4a50-80c4-502fbe2a932e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd7026d1-41d6-42a8-9ef1-a979bd3dbc29");

            migrationBuilder.DropColumn(
                name: "CompanyEmail",
                table: "CompanyInfo");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e64c769f-eb7a-4e9b-949d-a2a4e0360181", "db02f698-9189-467d-8497-99d28318292b", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae749f49-41f2-43ce-a863-9952af9fcc1b", "457b6934-6c0a-4d0f-858a-524d972d9675", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8fb48a5c-c35d-4d2a-876e-69d1d0e4bd73", "3c84e00a-4117-42f6-8de9-f2905aa7be13", "Vendor", null });
        }
    }
}
