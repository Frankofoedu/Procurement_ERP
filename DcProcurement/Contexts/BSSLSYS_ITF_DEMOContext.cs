using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DcProcurement.Contexts
{
    public partial class BSSLSYS_ITF_DEMOContext : DbContext
    {
        public BSSLSYS_ITF_DEMOContext()
        {
        }

        public BSSLSYS_ITF_DEMOContext(DbContextOptions<BSSLSYS_ITF_DEMOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Codestab> Codestab { get; set; }
        public virtual DbSet<Procreq1> Procreq1 { get; set; }
        public virtual DbSet<Procreq2> Procreq2 { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BSSLSYS_ITF_DEMO;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Codestab>(entity =>
            {
                entity.ToTable("codestab");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Copny)
                    .HasColumnName("copny")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Desc1)
                    .HasColumnName("desc1")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Option1)
                    .HasColumnName("option1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Option2)
                    .HasColumnName("option2")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Option3)
                    .HasColumnName("option3")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Procreq1>(entity =>
            {
                entity.ToTable("procreq1");

                entity.Property(e => e.Acperiod)
                    .HasColumnName("acperiod")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Acyear)
                    .HasColumnName("acyear")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.App)
                    .HasColumnName("app")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Appby)
                    .HasColumnName("appby")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Appdate)
                    .HasColumnName("appdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Auth)
                    .HasColumnName("auth")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Authby)
                    .HasColumnName("authby")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Authdate)
                    .HasColumnName("authdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Bal)
                    .HasColumnName("bal")
                    .HasColumnType("numeric(12, 2)");

                entity.Property(e => e.Bud)
                    .HasColumnName("bud")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Budapp)
                    .HasColumnName("budapp")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Budappby)
                    .HasColumnName("budappby")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Budappdate)
                    .HasColumnName("budappdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Budby)
                    .HasColumnName("budby")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Buddate)
                    .HasColumnName("buddate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Cancel)
                    .HasColumnName("cancel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Copny)
                    .HasColumnName("copny")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Costby)
                    .HasColumnName("costby")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Costdate)
                    .HasColumnName("costdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Custcode)
                    .HasColumnName("custcode")
                    .IsUnicode(false);

                entity.Property(e => e.Ddetail)
                    .HasColumnName("ddetail")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Deldate)
                    .HasColumnName("deldate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Inby)
                    .HasColumnName("inby")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Inidate)
                    .HasColumnName("inidate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Initiator)
                    .HasColumnName("initiator")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Initrank)
                    .HasColumnName("initrank")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Itemcost)
                    .HasColumnName("itemcost")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Matreq)
                    .HasColumnName("matreq")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Odept)
                    .HasColumnName("odept")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Rankap)
                    .HasColumnName("rankap")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rankau)
                    .HasColumnName("rankau")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rankba)
                    .HasColumnName("rankba")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rankbo)
                    .HasColumnName("rankbo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rankco)
                    .HasColumnName("rankco")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reqno)
                    .IsRequired()
                    .HasColumnName("reqno")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sentforapprval)
                    .HasColumnName("sentforapprval")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Serno)
                    .HasColumnName("serno")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Stop)
                    .HasColumnName("stop")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tdate)
                    .HasColumnName("tdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tenedate)
                    .HasColumnName("tenedate")
                    .HasColumnType("date");

                entity.Property(e => e.Tensdate)
                    .HasColumnName("tensdate")
                    .HasColumnType("date");

                entity.Property(e => e.Zcopny)
                    .HasColumnName("zcopny")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Procreq2>(entity =>
            {
                entity.ToTable("procreq2");

                entity.Property(e => e.Achead)
                    .HasColumnName("achead")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Acperiod)
                    .HasColumnName("acperiod")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Acyear)
                    .HasColumnName("acyear")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Amtrquest)
                    .HasColumnName("amtrquest")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Amtviredtodate)
                    .HasColumnName("amtviredtodate")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Bal)
                    .HasColumnName("bal")
                    .HasColumnType("numeric(12, 2)");

                entity.Property(e => e.Budamt)
                    .HasColumnName("budamt")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Budbalamt)
                    .HasColumnName("budbalamt")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Budfav)
                    .HasColumnName("budfav")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cancel)
                    .HasColumnName("cancel")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Commtodateamt)
                    .HasColumnName("commtodateamt")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Copny)
                    .HasColumnName("copny")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Custcode)
                    .HasColumnName("custcode")
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dept)
                    .HasColumnName("dept")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("numeric(19, 2)");

                entity.Property(e => e.Icode)
                    .HasColumnName("icode")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Idescr)
                    .HasColumnName("idescr")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Maindept)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Oamount)
                    .HasColumnName("oamount")
                    .HasColumnType("numeric(19, 2)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(19, 2)");

                entity.Property(e => e.Provbalamt)
                    .HasColumnName("provbalamt")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasColumnType("numeric(19, 2)");

                entity.Property(e => e.Quoted)
                    .HasColumnName("quoted")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('F')");

                entity.Property(e => e.Relamt)
                    .HasColumnName("relamt")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Reqno)
                    .IsRequired()
                    .HasColumnName("reqno")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Revamt)
                    .HasColumnName("revamt")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Serno)
                    .HasColumnName("serno")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Stop)
                    .HasColumnName("stop")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Subhead)
                    .HasColumnName("subhead")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Suggsup)
                    .HasColumnName("suggsup")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Suppamt)
                    .HasColumnName("suppamt")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Tdate)
                    .HasColumnName("tdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tenedate)
                    .HasColumnName("tenedate")
                    .HasColumnType("date")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tensdate)
                    .HasColumnName("tensdate")
                    .HasColumnType("date")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uofm)
                    .HasColumnName("uofm")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Vat)
                    .HasColumnName("vat")
                    .HasColumnType("numeric(19, 2)");

                entity.Property(e => e.Zcopny)
                    .HasColumnName("zcopny")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
