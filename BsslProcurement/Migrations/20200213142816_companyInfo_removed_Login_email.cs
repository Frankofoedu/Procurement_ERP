using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class companyInfo_removed_Login_email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "LoginEmail",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "CompanyInfo");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a040c669-fd0c-4345-a773-9638fee6f0ae", "57d27c1f-cb50-4096-9f81-bcb4fe808218", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ae17848-5155-40d3-9d14-d75b8da70e57", "6dd5ce73-7014-4ccb-9e0d-edbf3aed6b21", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16f9b09b-6b5f-43b5-811d-d34da059b65f", "284c3d94-5537-4e4d-be63-346429221dcf", "Vendor", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ae17848-5155-40d3-9d14-d75b8da70e57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16f9b09b-6b5f-43b5-811d-d34da059b65f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a040c669-fd0c-4345-a773-9638fee6f0ae");

            migrationBuilder.AddColumn<string>(
                name: "LoginEmail",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "CompanyInfo",
                type: "nvarchar(max)",
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
    }
}
