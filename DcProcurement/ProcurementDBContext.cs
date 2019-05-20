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
    }
}
