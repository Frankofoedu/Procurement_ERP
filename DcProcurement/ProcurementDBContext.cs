using Microsoft.EntityFrameworkCore;
using DcProcurement.JoinTables;
using System;

namespace DcProcurement
{
    public class ProcurementDBContext : DbContext
    {
        public ProcurementDBContext(DbContextOptions options)
           : base(options)
        { }


        #region Company Informations
        public CompanyInfo CompanyInfo { get; set; }
        public EquipmentDetails EquipmentDetails { get; set; }
        public ExperienceRecord ExperienceRecord { get; set; }
        public PersonnelDetails PersonnelDetails { get; set; }

        public SubmittedCriteria SubmittedCriteria { get; set; }

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

        public DbSet<PrequalificationPolicy> PrequalificationPolicies { get; set; }

        public CompanyInfoProcurementSubCategory  CompanyInfoProcurementSubCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyInfoProcurementSubCategory>().HasKey(sc => new { sc.CompanyInfoId, sc.ProcurementSubcategoryId });
        }
    }
}
