using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class usergroup_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60ecc651-89ca-4f3a-85e4-8528f3857499");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab0b3e6a-cbda-44e9-9b0a-7490e4ddd515");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc2fc37a-4feb-480f-a6e6-4eccd0e57bdd");

            migrationBuilder.AddColumn<string>(
                name: "Access",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Access", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "55bb663b-e823-4a33-880c-f49b7488732b", null, "2949e778-dc54-458f-90b5-ef23db7d1cf8", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Access", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "475974e5-4322-447a-b461-54cfac1b6612", null, "b58ed752-7bf2-428c-8cd3-17a2e59384e1", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Access", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86061be3-5007-4a0e-9f29-c34c5ff232f4", null, "f81b93a0-1cf1-43aa-83c5-7cf61e2c877c", "Vendor", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "475974e5-4322-447a-b461-54cfac1b6612");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55bb663b-e823-4a33-880c-f49b7488732b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86061be3-5007-4a0e-9f29-c34c5ff232f4");

            migrationBuilder.DropColumn(
                name: "Access",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab0b3e6a-cbda-44e9-9b0a-7490e4ddd515", "150c73c2-5e4c-4b76-b646-abebf0ed4819", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60ecc651-89ca-4f3a-85e4-8528f3857499", "86f01523-b672-419c-ba28-628e7c80752f", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc2fc37a-4feb-480f-a6e6-4eccd0e57bdd", "8e6d3d9c-d17e-4bc4-84f8-94c1379dc1e4", "Vendor", null });
        }
    }
}
