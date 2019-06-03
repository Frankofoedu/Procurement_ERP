﻿// <auto-generated />
using System;
using DcProcurement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BsslProcurement.Migrations
{
    [DbContext(typeof(ProcurementDBContext))]
    [Migration("20190527100800_itemUpdate")]
    partial class itemUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DcProcurement.ProcurementCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryDescription");

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ContractCategories");
                });

            modelBuilder.Entity("DcProcurement.ProcurementSubcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProcurementCategoryId");

                    b.Property<string>("SubcategoryDescription");

                    b.Property<string>("SubcategoryName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProcurementCategoryId");

                    b.ToTable("ContractSubcategories");
                });

            modelBuilder.Entity("DcProcurement.ProcurementCriteria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CriteriaDescription")
                        .IsRequired();

                    b.Property<int?>("MinValue");

                    b.Property<bool>("NeedsDocument");

                    b.Property<int?>("ProcurementItemId");

                    b.Property<bool>("isCompulsory");

                    b.HasKey("Id");

                    b.HasIndex("ProcurementItemId");

                    b.ToTable("ProcurementCriteria");
                });

            modelBuilder.Entity("DcProcurement.ProcurementGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ClosingDate");

                    b.Property<string>("GroupName");

                    b.Property<int>("NoOfCategory");

                    b.Property<DateTime?>("OpeningDate");

                    b.HasKey("Id");

                    b.ToTable("ProcurementGroups");
                });

            modelBuilder.Entity("DcProcurement.ProcurementItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProcurementSubcategoryId");

                    b.Property<DateTime?>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<string>("ItemName")
                        .IsRequired();

                    b.Property<int>("ProcurementGroupId");

                    b.HasKey("Id");

                    b.HasIndex("ProcurementSubcategoryId");

                    b.HasIndex("ProcurementGroupId");

                    b.ToTable("ProcurementItems");
                });

            modelBuilder.Entity("DcProcurement.ProcurementPortalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Legal");

                    b.Property<string>("PortalLogo");

                    b.Property<string>("PortalName");

                    b.HasKey("Id");

                    b.ToTable("ProcurementPortalInfo");
                });

            modelBuilder.Entity("DcProcurement.ProcurementSubcategory", b =>
                {
                    b.HasOne("DcProcurement.ProcurementCategory", "ProcurementCategory")
                        .WithMany("ContractSubcategories")
                        .HasForeignKey("ProcurementCategoryId");
                });

            modelBuilder.Entity("DcProcurement.ProcurementCriteria", b =>
                {
                    b.HasOne("DcProcurement.ProcurementItem", "ProcurementItem")
                        .WithMany("ProcurementCriterias")
                        .HasForeignKey("ProcurementItemId");
                });

            modelBuilder.Entity("DcProcurement.ProcurementItem", b =>
                {
                    b.HasOne("DcProcurement.ProcurementSubcategory", "ProcurementSubcategory")
                        .WithMany("ProcurementItems")
                        .HasForeignKey("ProcurementSubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DcProcurement.ProcurementGroup", "ProcurementGroup")
                        .WithMany("ProcurementItems")
                        .HasForeignKey("ProcurementGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}