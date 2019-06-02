using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class jointable_company_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyRegNo = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PostalAddress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    NatureOfBusiness = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    DateEstablishment = table.Column<DateTime>(nullable: false),
                    TIN = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactDesignation = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfoProcurementSubCategory",
                columns: table => new
                {
                    CompanyInfoId = table.Column<int>(nullable: false),
                    ProcurementSubcategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfoProcurementSubCategory", x => new { x.CompanyInfoId, x.ProcurementSubcategoryId });
                    table.ForeignKey(
                        name: "FK_CompanyInfoProcurementSubCategory_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyInfoProcurementSubCategory_ProcurementSubcategories_ProcurementSubcategoryId",
                        column: x => x.ProcurementSubcategoryId,
                        principalTable: "ProcurementSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyInfoId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentDetails_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyInfoId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ProjectDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    ContactPerson = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceRecord_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonnelDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    CV = table.Column<string>(nullable: true),
                    Certificate = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    CompanyInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelDetails_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyInfoId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmittedCriteria_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmittedCriteria_Criterias_CriteriaId",
                        column: x => x.CriteriaId,
                        principalTable: "Criterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfoProcurementSubCategory_ProcurementSubcategoryId",
                table: "CompanyInfoProcurementSubCategory",
                column: "ProcurementSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentDetails_CompanyInfoId",
                table: "EquipmentDetails",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceRecord_CompanyInfoId",
                table: "ExperienceRecord",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDetails_CompanyInfoId",
                table: "PersonnelDetails",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedCriteria_CompanyInfoId",
                table: "SubmittedCriteria",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedCriteria_CriteriaId",
                table: "SubmittedCriteria",
                column: "CriteriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInfoProcurementSubCategory");

            migrationBuilder.DropTable(
                name: "EquipmentDetails");

            migrationBuilder.DropTable(
                name: "ExperienceRecord");

            migrationBuilder.DropTable(
                name: "PersonnelDetails");

            migrationBuilder.DropTable(
                name: "SubmittedCriteria");

            migrationBuilder.DropTable(
                name: "CompanyInfo");
        }
    }
}
