using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using DcProcurement.Jobs;

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
        public DbSet<PrequalificationJob> PrequalificationJobs { get; set; }
        public DbSet<RequisitionJob> RequisitionJobs { get; set; }
        public DbSet<ProcurementJob> ProcurementJobs { get; set; }
        #endregion

        #region Requisition Tables
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<RequisitionItem> RequisitionItems { get; set; }
        public DbSet<ERFXSetup> ERFXSetups { get; set; }
        public DbSet<TechnicalERFXSetup> TechnicalERFXSetups { get; set; }
        public DbSet<FinancialERFXSetup> FinancialERFXSetups { get; set; }
        #endregion

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<PRNo> PRNos { get; set; }

        public DbSet<PrequalificationPolicy> PrequalificationPolicies { get; set; }

        public DbSet<CompanyInfoProcurementSubCategory> CompanyInfoProcurementSubCategory { get; set; }
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

            modelBuilder.Entity<Job>().ToTable("Jobs");
            modelBuilder.Entity<RequisitionJob>()
             .Property(e => e.RequisitionId)
             .HasColumnName("FK_Req_Job");

            modelBuilder.Entity<ProcurementJob>()
              .Property(e => e.RequisitionId)
              .HasColumnName("FK_Proc_Job");
            modelBuilder.Entity<PrequalificationJob>()
              .Property(e => e.CompanyInfoId)
              .HasColumnName(nameof(PrequalificationJob.CompanyInfoId));

            modelBuilder.Entity<RequisitionJob>().HasOne(x => x.Requisition).WithMany(x => x.RequisitionJobs).HasForeignKey(x => x.RequisitionId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProcurementJob>().HasOne(x => x.Requisition).WithMany(x => x.ProcurementJobs).HasForeignKey(x => x.RequisitionId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrequalificationJob>().HasOne(x => x.CompanyInfo).WithMany(x => x.PrequalificationJobs).HasForeignKey(x => x.CompanyInfoId).OnDelete(DeleteBehavior.Cascade);

            #region Seed Data

     

            //workflow types
            List<WorkflowType> ListWorkflowTypes = new List<WorkflowType>
            {
                new WorkflowType {Id = Constants.PrequalificationWorkFlowId, Name = Constants.PrequalificationWorkFlow, Code = "0002"},
                new WorkflowType {Id = Constants.RequisitionWorkflowId, Name = Constants.RequisitionWorkflow, Code = "0003"},
                new WorkflowType { Id = Constants.ProcurementWorkflowId, Name = Constants.ProcurementWorkflow, Code = "0004" },
            };
            //seed workflow types
            modelBuilder.Entity<WorkflowType>().HasData(ListWorkflowTypes);


            //procurement actions
            List<WorkflowAction> listProcActions = new List<WorkflowAction>
            {
                new WorkflowAction{ Id = Constants.ProcurementCostingId, Name = Constants.ProcurementCosting, Description = "For Procurement Costing"},

                new WorkflowAction{ Id = Constants.BudgetaryControlId, Name = Constants.BudgetaryControl, Description = "For Budgetary Control"},
                new WorkflowAction{ Id = Constants.ApproverId, Name = Constants.Approver, Description = "For Approval"},
                new WorkflowAction{ Id = Constants.AuthorizerId, Name = Constants.Authorizer, Description = "For Authorization"},
                new WorkflowAction{ Id = Constants.ApproveRaiseERFxId, Name = Constants.ApproveRaiseERFx, Description = "For Approval To Raising Erfx"},
                new WorkflowAction{ Id = Constants.RaiseERFxId, Name = Constants.RaiseERFx, Description = "For Raising Erfx"},
            };
            //seed workflow for procurement
            modelBuilder.Entity<WorkflowAction>().HasData(listProcActions);



            var lisProcurementWorkflow = new List<Workflow>()
            {
                new Workflow { Id = 100, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.ProcurementCostingId, Step= 1},
                new Workflow { Id = 200, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.BudgetaryControlId , Step= 2},
                new Workflow { Id = 300, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.ApproverId, Step= 3},
                new Workflow { Id = 400, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.AuthorizerId, Step= 4},
                new Workflow { Id = 500, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.ApproveRaiseERFxId, Step= 5},
                new Workflow { Id = 600, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.RaiseERFxId, Step= 6},

            };

            modelBuilder.Entity<Workflow>().HasData(lisProcurementWorkflow);


            //seed roles
            var roles = new List<IdentityRole> {
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Constants.Role.Admin },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Constants.Role.Staff },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Constants.Role.Vendor }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            #endregion


            base.OnModelCreating(modelBuilder);

        }
    }
}
