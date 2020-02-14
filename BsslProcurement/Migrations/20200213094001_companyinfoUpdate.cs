using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class companyinfoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ecc0b0b-df62-4b33-9b9f-a7bd119a3702");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce4d1de1-f617-4de7-9c68-303b263f7575");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d76dda33-6a13-4619-b229-fd1d0cae5090");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AuthorizedDate",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AuthorizedDesignation",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorizedName",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BVN",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankSortCode",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CertificateOfIncorpId",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyProfileId",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyWebsite",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentNetAsset",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentNetProfit",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentTurnover",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaxNumber",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasHSE",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasNCEC",
                table: "CompanyInfo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LoginEmail",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfEmployees",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnershipStructure",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentCompanyName",
                table: "CompanyInfo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxRegistrationId",
                table: "CompanyInfo",
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

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfo_CertificateOfIncorpId",
                table: "CompanyInfo",
                column: "CertificateOfIncorpId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfo_CompanyProfileId",
                table: "CompanyInfo",
                column: "CompanyProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfo_TaxRegistrationId",
                table: "CompanyInfo",
                column: "TaxRegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfo_Attachments_CertificateOfIncorpId",
                table: "CompanyInfo",
                column: "CertificateOfIncorpId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfo_Attachments_CompanyProfileId",
                table: "CompanyInfo",
                column: "CompanyProfileId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfo_Attachments_TaxRegistrationId",
                table: "CompanyInfo",
                column: "TaxRegistrationId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfo_Attachments_CertificateOfIncorpId",
                table: "CompanyInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfo_Attachments_CompanyProfileId",
                table: "CompanyInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfo_Attachments_TaxRegistrationId",
                table: "CompanyInfo");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInfo_CertificateOfIncorpId",
                table: "CompanyInfo");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInfo_CompanyProfileId",
                table: "CompanyInfo");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInfo_TaxRegistrationId",
                table: "CompanyInfo");

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
                name: "AccountNumber",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "AuthorizedDate",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "AuthorizedDesignation",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "AuthorizedName",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "BVN",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "BankSortCode",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CertificateOfIncorpId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CompanyProfileId",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CompanyWebsite",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CurrentNetAsset",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CurrentNetProfit",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "CurrentTurnover",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "FaxNumber",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "HasHSE",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "HasNCEC",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "LoginEmail",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployees",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "OwnershipStructure",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "ParentCompanyName",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "TaxRegistrationId",
                table: "CompanyInfo");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ecc0b0b-df62-4b33-9b9f-a7bd119a3702", "eaa918fc-8aa1-4978-9741-c9ca69b99394", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce4d1de1-f617-4de7-9c68-303b263f7575", "e7561fae-0c54-43f5-9658-ae93c9efb53c", "Staff", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d76dda33-6a13-4619-b229-fd1d0cae5090", "154482fe-35a9-4d55-b056-2bb0039b4447", "Vendor", null });
        }
    }
}
