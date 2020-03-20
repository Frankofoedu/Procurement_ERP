using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class usergroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "302f96c1-8bba-49f6-850b-06aa12db7ef6", "74c0d5e6-aeda-44d4-bbca-c65fe59f8484", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4df0ca2b-6d81-4a1e-85ac-f43fcb60cd0e", "b50487aa-cf03-4ac0-9bed-9dda6d194ac4", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2535144f-a97c-42e1-a619-7ebc0945396e", "83a99fa9-4bc9-4731-845b-4a9726cfbe23", "Vendor", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2535144f-a97c-42e1-a619-7ebc0945396e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "302f96c1-8bba-49f6-850b-06aa12db7ef6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4df0ca2b-6d81-4a1e-85ac-f43fcb60cd0e");

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
    }
}
