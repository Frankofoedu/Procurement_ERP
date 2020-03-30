using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using DcProcurement.Jobs;
using DcProcurement.Users;

namespace DcProcurement
{
    public class ProcurementDBContext : IdentityDbContext<User, UserRole, string>
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
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<StaffUserGroup>  StaffUserGroups { get; set; }
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

            #region Many to many between staff and groups
            modelBuilder.Entity<StaffUserGroup>()
                .HasKey(bc => new { bc.StaffId, bc.UserGroupId });
            modelBuilder.Entity<StaffUserGroup>()
                .HasOne(bc => bc.Staff)
                .WithMany(b => b.UserGroups)
                .HasForeignKey(bc => bc.StaffId);
            modelBuilder.Entity<StaffUserGroup>()
                .HasOne(bc => bc.UserGroup)
                .WithMany(c => c.Staffs)
                .HasForeignKey(bc => bc.UserGroupId);
            #endregion

            modelBuilder.Entity<Attachment>().Property(m => m.DateCreated).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Requisition>().Property(m => m.DateCreated).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<CompanyInfo>().HasOne(m => m.Vendor).WithOne(n => n.CompanyInfo).HasForeignKey<VendorUser>(l => l.CompanyInfoId);

            modelBuilder.Entity<RequisitionItem>().HasOne(m => m.Vendor).WithMany(n => n.RequisitionItems).HasForeignKey(w => w.VendorId);

            modelBuilder.Entity<Job>().ToTable("Jobs");
            //modelBuilder.Entity<RequisitionJob>()
            // .Property(e => e.RequisitionId)
            // .HasColumnName("FK_Req_Job");

            //modelBuilder.Entity<ProcurementJob>()
            //  .Property(e => e.RequisitionId)
            //  .HasColumnName("FK_Proc_Job");
            //modelBuilder.Entity<PrequalificationJob>()
            //  .Property(e => e.CompanyInfoId)
            //  .HasColumnName(nameof(PrequalificationJob.CompanyInfoId));

            //modelBuilder.Entity<RequisitionJob>().HasOne(x => x.Requisition).WithMany(x => x.RequisitionJobs).HasForeignKey(x => x.RequisitionId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProcurementJob>().HasOne(x => x.Requisition).WithMany(x => x.ProcurementJobs).HasForeignKey(x => x.RequisitionId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<PrequalificationJob>().HasOne(x => x.CompanyInfo).WithMany(x => x.PrequalificationJobs).HasForeignKey(x => x.CompanyInfoId).OnDelete(DeleteBehavior.Cascade);

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
                new WorkflowAction{ Id = Constants.ProcurementCostingActionId, Name = Constants.ProcurementCostingAction, Description = "For Procurement Costing"},

                new WorkflowAction{ Id = Constants.BudgetaryControlActionId, Name = Constants.BudgetaryControlAction, Description = "For Budgetary Control"},
                new WorkflowAction{ Id = Constants.ApproverActionId, Name = Constants.ApproverAction, Description = "For Approval"},
                new WorkflowAction{ Id = Constants.AuthorizerActionId, Name = Constants.AuthorizerAction, Description = "For Authorization"},
                new WorkflowAction{ Id = Constants.ApproveRaiseERFxActionId, Name = Constants.ApproveRaiseERFxAction, Description = "For Approval To Raising Erfx"},
                new WorkflowAction{ Id = Constants.RaiseERFxActionId, Name = Constants.RaiseERFxAction, Description = "For Raising Erfx"},
            };
            //seed workflow actions for procurement
            modelBuilder.Entity<WorkflowAction>().HasData(listProcActions);



            var lisProcurementWorkflow = new List<Workflow>()
            {
                new Workflow { Id = 100, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.ProcurementCostingActionId, Step= 1},
                new Workflow { Id = 200, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.BudgetaryControlActionId , Step= 2},
                new Workflow { Id = 300, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.ApproverActionId, Step= 3},
                new Workflow { Id = 400, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.AuthorizerActionId, Step= 4},
                new Workflow { Id = 500, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.ApproveRaiseERFxActionId, Step= 5},
                new Workflow { Id = 600, WorkflowTypeId = Constants.ProcurementWorkflowId, WorkflowActionId = Constants.RaiseERFxActionId, Step= 6},

            };

            modelBuilder.Entity<Workflow>().HasData(lisProcurementWorkflow);

            



                    //seed roles
                    var roles = new List<UserRole> {
                new UserRole { Id = "d6dde6fb-8354-409d-b700-40da947c88d8", Name = Constants.Role.Admin },
                new UserRole { Id = "02174cf0-9412-4cfe-afbf-59f706d72c8e", Name = Constants.Role.Staff },
                new UserRole { Id = "19879c37-bc22-4ed8-a7be-8819026aa3ce", Name = Constants.Role.Vendor }
            };
            modelBuilder.Entity<UserRole>().HasData(roles);
            #endregion


            base.OnModelCreating(modelBuilder);

        }
    }
}
