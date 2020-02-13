using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class companyInfoNewUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c02a5d6-d171-4d1f-b56c-a5889601999a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e86f628-30b1-431f-b0d3-1f678f70181c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95fa4d33-7066-4543-9620-ae7e92978cca");

            migrationBuilder.DropColumn(
                name: "CV",
                table: "PersonnelDetails");

            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "PersonnelDetails");

            migrationBuilder.DropColumn(
                name: "Passport",
                table: "PersonnelDetails");

            migrationBuilder.AddColumn<int>(
                name: "CVId",
                table: "PersonnelDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "PersonnelDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassportId",
                table: "PersonnelDetails",
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

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDetails_CVId",
                table: "PersonnelDetails",
                column: "CVId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDetails_CertificateId",
                table: "PersonnelDetails",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDetails_PassportId",
                table: "PersonnelDetails",
                column: "PassportId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelDetails_Attachments_CVId",
                table: "PersonnelDetails",
                column: "CVId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelDetails_Attachments_CertificateId",
                table: "PersonnelDetails",
                column: "CertificateId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelDetails_Attachments_PassportId",
                table: "PersonnelDetails",
                column: "PassportId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelDetails_Attachments_CVId",
                table: "PersonnelDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelDetails_Attachments_CertificateId",
                table: "PersonnelDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelDetails_Attachments_PassportId",
                table: "PersonnelDetails");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelDetails_CVId",
                table: "PersonnelDetails");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelDetails_CertificateId",
                table: "PersonnelDetails");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelDetails_PassportId",
                table: "PersonnelDetails");

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
                name: "CVId",
                table: "PersonnelDetails");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "PersonnelDetails");

            migrationBuilder.DropColumn(
                name: "PassportId",
                table: "PersonnelDetails");

            migrationBuilder.AddColumn<string>(
                name: "CV",
                table: "PersonnelDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certificate",
                table: "PersonnelDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "PersonnelDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8e86f628-30b1-431f-b0d3-1f678f70181c", "37eb03e1-8959-4853-9174-d5d03bfe7e1d", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c02a5d6-d171-4d1f-b56c-a5889601999a", "bf4bd0fe-c8ce-492a-8691-dd4faa0cc086", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "95fa4d33-7066-4543-9620-ae7e92978cca", "d8c49081-fabe-4657-b2e0-dbe1dd162f3b", "Vendor", null });
        }
    }
}
