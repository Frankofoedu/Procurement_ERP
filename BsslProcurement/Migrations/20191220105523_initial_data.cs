using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BsslProcurement.Migrations
{
    public partial class initial_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyRegNo = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
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
                    ContactEmail = table.Column<string>(nullable: true),
                    VendorId = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    Disqualified = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrequalificationPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoOfCategory = table.Column<int>(nullable: false),
                    PrequalificationStartDate = table.Column<DateTime>(nullable: false),
                    PrequalificationEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrequalificationPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRNos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequisitionCode = table.Column<string>(nullable: true),
                    LastUsedSerialNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRNos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcurementCategoryCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true),
                    NoOfCategory = table.Column<int>(nullable: false),
                    OpeningDate = table.Column<DateTime>(nullable: true),
                    ClosingDate = table.Column<DateTime>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementPortalInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortalName = table.Column<string>(nullable: true),
                    PortalLogo = table.Column<string>(nullable: true),
                    Legal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementPortalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requisitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isSubmitted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PRNumber = table.Column<string>(nullable: false),
                    ProcurementType = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    RequesterType = table.Column<string>(nullable: false),
                    RequesterValue = table.Column<string>(nullable: false),
                    RequiredAtDepartment = table.Column<string>(nullable: false),
                    PreparedBy = table.Column<string>(nullable: false),
                    PreparedByRank = table.Column<string>(nullable: true),
                    PreparedFor = table.Column<string>(nullable: false),
                    PreparedForRank = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    ProcurementMethod = table.Column<string>(nullable: true),
                    ProcessType = table.Column<string>(nullable: true),
                    ERFx = table.Column<string>(nullable: true),
                    isPriced = table.Column<bool>(nullable: false),
                    isBudgetCleared = table.Column<bool>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    LoggedInUserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    StaffCode = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    CompanyInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyInfoId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    CV = table.Column<string>(nullable: true),
                    Certificate = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    VerificationState = table.Column<int>(nullable: false)
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
                name: "ProcurementSubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcurementSubCategoryCode = table.Column<string>(nullable: true),
                    ProcurementCategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcurementSubcategories_ProcurementCategories_ProcurementCategoryId",
                        column: x => x.ProcurementCategoryId,
                        principalTable: "ProcurementCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ERFXSetups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ErfxDate = table.Column<DateTime>(nullable: true),
                    ProjectTitle = table.Column<string>(nullable: true),
                    BidType = table.Column<int>(nullable: false),
                    Submitted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERFXSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ERFXSetups_Requisitions_Id",
                        column: x => x.Id,
                        principalTable: "Requisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowActionId = table.Column<int>(nullable: true),
                    WorkflowTypeId = table.Column<int>(nullable: true),
                    Step = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workflows_WorkflowActions_WorkflowActionId",
                        column: x => x.WorkflowActionId,
                        principalTable: "WorkflowActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workflows_WorkflowTypes_WorkflowTypeId",
                        column: x => x.WorkflowTypeId,
                        principalTable: "WorkflowTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkFlowStep = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DoneDate = table.Column<DateTime>(nullable: true),
                    JobStatus = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    StaffId = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    RequisitionId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<int>(nullable: true),
                    StaffId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_Requisitions_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_AspNetUsers_StaffId1",
                        column: x => x.StaffId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequisitionItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitOfMeasurement = table.Column<string>(nullable: true),
                    VendorId = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<double>(nullable: false),
                    RequisitionId = table.Column<int>(nullable: true),
                    AttachmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisitionItems_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisitionItems_Requisitions_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequisitionItems_AspNetUsers_VendorId",
                        column: x => x.VendorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(nullable: false),
                    ProcurementSubcategoryId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ProcurementSubcategories_ProcurementSubcategoryId",
                        column: x => x.ProcurementSubcategoryId,
                        principalTable: "ProcurementSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialERFXSetups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BidStartDate = table.Column<DateTime>(nullable: true),
                    BidEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialERFXSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialERFXSetups_ERFXSetups_Id",
                        column: x => x.Id,
                        principalTable: "ERFXSetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalERFXSetups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BidStartDate = table.Column<DateTime>(nullable: true),
                    BidEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalERFXSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalERFXSetups_ERFXSetups_Id",
                        column: x => x.Id,
                        principalTable: "ERFXSetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<int>(nullable: false),
                    StaffId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowStaffs_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowStaffs_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Criterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaDescription = table.Column<string>(nullable: false),
                    MinValue = table.Column<int>(nullable: true),
                    isCompulsory = table.Column<bool>(nullable: false),
                    NeedsDocument = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ProcurementCategoryId = table.Column<int>(nullable: true),
                    ItemId = table.Column<int>(nullable: true),
                    ProcurementSubcategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterias_ProcurementCategories_ProcurementCategoryId",
                        column: x => x.ProcurementCategoryId,
                        principalTable: "ProcurementCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Criterias_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Criterias_ProcurementSubcategories_ProcurementSubcategoryId",
                        column: x => x.ProcurementSubcategoryId,
                        principalTable: "ProcurementSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcurementItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: true),
                    ProcurementGroupId = table.Column<int>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    ProcurementSubcategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcurementItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcurementItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcurementItems_ProcurementGroups_ProcurementGroupId",
                        column: x => x.ProcurementGroupId,
                        principalTable: "ProcurementGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcurementItems_ProcurementSubcategories_ProcurementSubcategoryId",
                        column: x => x.ProcurementSubcategoryId,
                        principalTable: "ProcurementSubcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyInfoId = table.Column<int>(nullable: false),
                    CriteriaId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    VerificationState = table.Column<int>(nullable: false)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75eb136f-02e0-44a0-adf4-768d4f5e94b9", "c6b5caa0-eea6-45d2-8b42-980df2e33fea", "Admin", null },
                    { "3e34bbc9-7d75-4fdf-ba9c-58c3a9df2767", "3f4736ac-4a7b-466b-8656-96febdfa8006", "Staff", null },
                    { "7654e572-6e1c-4c6b-8d0c-7366cee70dda", "90b40fd6-fa4f-4e2a-9507-ccf4ec830e02", "Vendor", null }
                });

            migrationBuilder.InsertData(
                table: "Requisitions",
                columns: new[] { "Id", "Date", "DeliveryDate", "Description", "ERFx", "LoggedInUserId", "PRNumber", "PreparedBy", "PreparedByRank", "PreparedFor", "PreparedForRank", "ProcessType", "ProcurementMethod", "ProcurementType", "Purpose", "RequesterType", "RequesterValue", "RequiredAtDepartment", "Status", "isBudgetCleared", "isPriced", "isSubmitted" },
                values: new object[] { 22, new DateTime(2019, 12, 20, 11, 55, 22, 20, DateTimeKind.Local).AddTicks(7882), new DateTime(2019, 12, 20, 11, 55, 22, 20, DateTimeKind.Local).AddTicks(9010), "sample requisition", null, null, "000222", "John O", "HOD", "Abbah", "Hod", null, null, null, "For general stores", "Division", "kkkkkd", "head office", null, null, false, true });

            migrationBuilder.InsertData(
                table: "WorkflowTypes",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { 3, "0001", null, "Procurement" },
                    { 2, "0002", null, "Prequalification" },
                    { 1, "0003", null, "Requisition" }
                });

            migrationBuilder.InsertData(
                table: "RequisitionItems",
                columns: new[] { "Id", "AttachmentId", "Description", "Quantity", "RequisitionId", "StoreItemCode", "UnitOfMeasurement", "UnitPrice", "VendorId" },
                values: new object[] { 12, null, "biro", 22, 22, "22", null, 0.0, null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyInfoId",
                table: "AspNetUsers",
                column: "CompanyInfoId",
                unique: true,
                filter: "[CompanyInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInfoProcurementSubCategory_ProcurementSubcategoryId",
                table: "CompanyInfoProcurementSubCategory",
                column: "ProcurementSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_ProcurementCategoryId",
                table: "Criterias",
                column: "ProcurementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_ItemId",
                table: "Criterias",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterias_ProcurementSubcategoryId",
                table: "Criterias",
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
                name: "IX_Items_ProcurementSubcategoryId",
                table: "Items",
                column: "ProcurementSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_StaffId",
                table: "Jobs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RequisitionId",
                table: "Jobs",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyInfoId",
                table: "Jobs",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_StaffId1",
                table: "Jobs",
                column: "StaffId1");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelDetails_CompanyInfoId",
                table: "PersonnelDetails",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementItems_ItemId",
                table: "ProcurementItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementItems_ProcurementGroupId",
                table: "ProcurementItems",
                column: "ProcurementGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementItems_ProcurementSubcategoryId",
                table: "ProcurementItems",
                column: "ProcurementSubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcurementSubcategories_ProcurementCategoryId",
                table: "ProcurementSubcategories",
                column: "ProcurementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionItems_AttachmentId",
                table: "RequisitionItems",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionItems_RequisitionId",
                table: "RequisitionItems",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionItems_VendorId",
                table: "RequisitionItems",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedCriteria_CompanyInfoId",
                table: "SubmittedCriteria",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedCriteria_CriteriaId",
                table: "SubmittedCriteria",
                column: "CriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_WorkflowActionId",
                table: "Workflows",
                column: "WorkflowActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_WorkflowTypeId",
                table: "Workflows",
                column: "WorkflowTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStaffs_StaffId",
                table: "WorkflowStaffs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStaffs_WorkflowId",
                table: "WorkflowStaffs",
                column: "WorkflowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompanyInfoProcurementSubCategory");

            migrationBuilder.DropTable(
                name: "EquipmentDetails");

            migrationBuilder.DropTable(
                name: "ExperienceRecord");

            migrationBuilder.DropTable(
                name: "FinancialERFXSetups");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "PersonnelDetails");

            migrationBuilder.DropTable(
                name: "PrequalificationPolicies");

            migrationBuilder.DropTable(
                name: "PRNos");

            migrationBuilder.DropTable(
                name: "ProcurementItems");

            migrationBuilder.DropTable(
                name: "ProcurementPortalInfo");

            migrationBuilder.DropTable(
                name: "RequisitionItems");

            migrationBuilder.DropTable(
                name: "SubmittedCriteria");

            migrationBuilder.DropTable(
                name: "TechnicalERFXSetups");

            migrationBuilder.DropTable(
                name: "WorkflowStaffs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ProcurementGroups");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Criterias");

            migrationBuilder.DropTable(
                name: "ERFXSetups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Requisitions");

            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.DropTable(
                name: "WorkflowActions");

            migrationBuilder.DropTable(
                name: "WorkflowTypes");

            migrationBuilder.DropTable(
                name: "ProcurementSubcategories");

            migrationBuilder.DropTable(
                name: "ProcurementCategories");
        }
    }
}
