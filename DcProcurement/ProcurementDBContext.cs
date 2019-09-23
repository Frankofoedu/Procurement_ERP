using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

            modelBuilder.Entity<Workflow>().HasOne(m => m.StaffToAssign).WithMany(n => n.StaffWorkflows).HasForeignKey(w => w.StaffId);

            modelBuilder.Entity<Workflow>().HasOne(m => m.AlternativeStaffToAssign).WithMany(n => n.AdditionalStaffWorkflows).HasForeignKey(w => w.AlternativeStaffId);

            base.OnModelCreating(modelBuilder);

        }
    }
}
