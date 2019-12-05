using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace DcProcurement
{
    public class ProcurementDBContext : IdentityDbContext<User>
    {
        public ProcurementDBContext(DbContextOptions<ProcurementDBContext> options)
           : base(options)
        { }


        #region Company Informations
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        public DbSet<EquipmentDetails> EquipmentDetails { get; set; }
        public DbSet<ExperienceRecord> ExperienceRecord { get; set; }
        public DbSet<PersonnelDetails> PersonnelDetails { get; set; }

        public DbSet<SubmittedCriteria> SubmittedCriteria { get; set; }

        #endregion

        #region Criteria Tables

        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<CategoryCriteria> CategoryCriterias { get; set; }
        public DbSet<SubCategoryCriteria> SubCategoryCriterias { get; set; }
        public DbSet<ItemCriteria> ItemCriterias { get; set; }

        #endregion

        #region Procurement Tables

        public DbSet<ProcurementGroup> ProcurementGroups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ProcurementItem> ProcurementItems { get; set; }
        public DbSet<ProcurementPortalInfo> ProcurementPortalInfo { get; set; }
        public DbSet<ProcurementCategory> ProcurementCategories { get; set; }
        public DbSet<ProcurementSubcategory> ProcurementSubcategories { get; set; }

        #endregion

        #region Workflow Tables
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowType> WorkflowTypes { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
        public DbSet<WorkflowStaff> WorkflowStaffs { get; set; }
        #endregion

        #region Users Tables
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<VendorUser> Vendors { get; set; }
        #endregion

        #region Jobs Tables
        public DbSet<Job> Jobs { get; set; }
        public DbSet<PrequalificationJob> PrequalificationJobs { get; set; }
        #endregion

        #region Requisition Tables
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<RequisitionItem> RequisitionItems { get; set; }
        #endregion

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<PRNo> PRNos { get; set; }

        public DbSet<PrequalificationPolicy> PrequalificationPolicies { get; set; }

        public DbSet<CompanyInfoProcurementSubCategory>  CompanyInfoProcurementSubCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Many to many configuration for company to procurementsubcategory

            modelBuilder.Entity<CompanyInfoProcurementSubCategory>().HasKey(sc => new { sc.CompanyInfoId, sc.ProcurementSubcategoryId });
            modelBuilder.Entity<CompanyInfoProcurementSubCategory>().HasOne(x => x.CompanyInfo).WithMany(m => m.CompanyInfoSelectedSubcategory);
            modelBuilder.Entity<CompanyInfoProcurementSubCategory>().HasOne(x => x.ProcurementSubcategory).WithMany(m => m.CompanyInfos);

            #endregion

            modelBuilder.Entity<Attachment>().Property(m => m.DateCreated).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Requisition>().Property(m => m.DateCreated).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<CompanyInfo>().HasOne(m => m.Vendor).WithOne(n => n.CompanyInfo).HasForeignKey<VendorUser>(l => l.CompanyInfoId);

            modelBuilder.Entity<RequisitionItem>().HasOne(m => m.Vendor).WithMany(n => n.RequisitionItems).HasForeignKey(w => w.VendorId);


            #region Seed Data

            modelBuilder.Entity<RequisitionItem>().HasData(
                new RequisitionItem
                    {
                        Description = "biro",
                        Quantity = 22,
                        RequisitionId = 22,
                        StoreItemCode = "22",
                        Id = 12
                        
                    }
                );
            modelBuilder.Entity<Requisition>().HasData(new Requisition
            {
                Id = 22,
                Date = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Description = "sample requisition",
                isSubmitted = true,
                PreparedBy = "John O",
                PreparedByRank = "HOD",
                PreparedForRank = "Hod",
                PRNumber = "000222",
                Purpose = "For general stores",
                RequiredAtDepartment = "head office",
                RequesterValue = "kkkkkd",
                PreparedFor = "Abbah",
                RequesterType = "Division"            


            });

            List<WorkflowType> ListWorkflowTypes = new List<WorkflowType> 
            { 
                new WorkflowType { Id = 1, Name = Constants.ItemPricingWorkflow, Code = "0001" }, 
                new WorkflowType{Id = 2, Name = Constants.PrequalificationWorkFlow, Code = "0002"},
                new WorkflowType{Id = 3, Name = Constants.RequisitionWorkflow, Code = "0003"}
            };
                

            modelBuilder.Entity<WorkflowType>().HasData(ListWorkflowTypes);
            #endregion

            base.OnModelCreating(modelBuilder);

        }
    }
}
