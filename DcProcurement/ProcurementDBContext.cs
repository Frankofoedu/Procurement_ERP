using Microsoft.EntityFrameworkCore;
using System;

namespace DcProcurement
{
    public class ProcurementDBContext : DbContext
    {
        public ProcurementDBContext(DbContextOptions options)
           : base(options)
        { }

        public DbSet<ProcurementGroup> ProcurementGroups { get; set; }
        public DbSet<ProcurementItem> ProcurementItems { get; set; }
        public DbSet<ProcurementCriteria> ProcurementCriteria { get; set; }
        public DbSet<ProcurementPortalInfo> ProcurementPortalInfo { get; set; }
        public DbSet<ContractCategory> ContractCategories { get; set; }
        public DbSet<ContractSubcategory> ContractSubcategories { get; set; }
    }
}
