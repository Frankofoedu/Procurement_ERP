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
        public virtual DbSet<Accust> Accusts { get; set; }
        public virtual DbSet<CodestabRel> CodestabRel { get; set; }
        public virtual DbSet<Joborder> Joborder { get; set; }
        public virtual DbSet<Procreq1> Procreq1 { get; set; }
        public virtual DbSet<Procreq2> Procreq2 { get; set; }
        public virtual DbSet<Useracct> Useracct { get; set; }
        public virtual DbSet<Compdata> Compdata { get; set; }
        public virtual DbSet<Stafftab> Stafftab { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }


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

            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.ToTable("uom");

                entity.HasIndex(e => e.Ucode)
                    .HasName("ucode");

                entity.Property(e => e.Copny)
                    .HasColumnName("copny")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Ucode)
                    .IsRequired()
                    .HasColumnName("ucode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Uname)
                    .HasColumnName("uname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Zcopny)
                    .HasColumnName("zcopny")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Compdata>(entity =>
            {
                entity.HasKey(e => e.Compcode);

                entity.ToTable("COMPDATA");

                entity.Property(e => e.Compcode)
                    .HasColumnName("compcode")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Accode)
                    .HasColumnName("ACCODE")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Acyear)
                    .HasColumnName("ACYEAR")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Acyear2)
                    .HasColumnName("ACYEAR2")
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Allocation)
                    .HasColumnName("ALLOCATION")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Assetcurrent)
                    .HasColumnName("ASSETCURRENT")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Assetscrcu)
                    .HasColumnName("ASSETSCRCU")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Caddr)
                    .HasColumnName("CADDR")
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Caddr2)
                    .HasColumnName("CADDR2")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Caddr3)
                    .HasColumnName("CADDR3")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Capvat)
                    .HasColumnName("CAPVAT")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Clclosed)
                    .HasColumnName("CLCLOSED")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Closedate)
                    .HasColumnName("CLOSEDATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cname)
                    .IsRequired()
                    .HasColumnName("CNAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Complogo)
                    .HasColumnName("COMPLOGO")
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comptype)
                    .HasColumnName("comptype")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Compylogo).HasColumnName("compylogo");

                entity.Property(e => e.Coytype)
                    .HasColumnName("COYTYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Curperiod)
                    .HasColumnName("CURPERIOD")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Depcurrent)
                    .HasColumnName("DEPCURRENT")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Deplimit)
                    .HasColumnName("DEPLIMIT")
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Faxtel)
                    .HasColumnName("faxtel")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Intsusp)
                    .HasColumnName("INTSUSP")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lastdepp)
                    .HasColumnName("LASTDEPP")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lastupd)
                    .HasColumnName("LASTUPD")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Names)
                    .HasColumnName("NAMES")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Paytax)
                    .HasColumnName("PAYTAX")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Reporthead)
                    .HasColumnName("reporthead")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rsasset)
                    .HasColumnName("RSASSET")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Systemname)
                    .HasColumnName("SYSTEMNAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tindexdate)
                    .HasColumnName("TINDEXDATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Webaddr)
                    .HasColumnName("webaddr")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Useracct>(entity =>
            {
                entity.HasKey(e => e.Pkvalue);

                entity.ToTable("USERACCT");

                entity.Property(e => e.Pkvalue)
                    .HasColumnName("pkvalue")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Accslvl)
                    .HasColumnName("accslvl")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AcctPay).HasColumnName("acct_pay");

                entity.Property(e => e.AcctRec).HasColumnName("acct_rec");

                entity.Property(e => e.AcctSetup).HasColumnName("acct_setup");

                entity.Property(e => e.Addybook).HasColumnName("addybook");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.Analysis).HasColumnName("analysis");

                entity.Property(e => e.Audit).HasColumnName("audit");

                entity.Property(e => e.BankCash).HasColumnName("bank_cash");

                entity.Property(e => e.Budget).HasColumnName("budget");

                entity.Property(e => e.Compcode)
                    .IsRequired()
                    .HasColumnName("compcode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Coordination).HasColumnName("coordination");

                entity.Property(e => e.Datein)
                    .HasColumnName("datein")
                    .HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Expiredate)
                    .HasColumnName("expiredate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixdAsst).HasColumnName("fixd_asst");

                entity.Property(e => e.Gledger).HasColumnName("gledger");

                entity.Property(e => e.GovBudget).HasColumnName("gov_budget");

                entity.Property(e => e.Grpcode)
                    .HasColumnName("grpcode")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Lastlogin)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Loggedon).HasColumnName("loggedon");

                entity.Property(e => e.Modidate)
                    .HasColumnName("modidate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Newlogin).HasColumnName("newlogin");

                entity.Property(e => e.Nvrexpire).HasColumnName("nvrexpire");

                entity.Property(e => e.Patsalinvo).HasColumnName("patsalinvo");

                entity.Property(e => e.Procurement)
                    .HasColumnName("procurement")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Project).HasColumnName("project");

                entity.Property(e => e.Prtaccess).HasColumnName("prtaccess");

                entity.Property(e => e.Pwd)
                    .HasColumnName("pwd")
                    .IsUnicode(false);

                entity.Property(e => e.Raccess).HasColumnName("raccess");

                entity.Property(e => e.Revenue).HasColumnName("revenue");

                entity.Property(e => e.Rptaccess).HasColumnName("rptaccess");

                entity.Property(e => e.StockInvent).HasColumnName("stock_invent");

                entity.Property(e => e.StockReq).HasColumnName("stock_req");

                entity.Property(e => e.Suspend).HasColumnName("suspend");

                entity.Property(e => e.Uaccess).HasColumnName("uaccess");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Warrant).HasColumnName("warrant");

                entity.Property(e => e.Zcopny)
                    .HasColumnName("zcopny")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Codestab>(entity =>
            {
                entity.HasKey(e => e.Codeid);

                entity.ToTable("codestab");

                entity.Property(e => e.Codeid)
                    .HasColumnName("codeid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Appto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Appto1)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Barr)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Compcode)
                    .HasColumnName("compcode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Consolidate).HasColumnName("consolidate");

                entity.Property(e => e.Desc1)
                    .HasColumnName("desc1")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Desc2)
                    .HasColumnName("desc2")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Desc3)
                    .HasColumnName("desc3")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Memoaddr)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Option1)
                    .HasColumnName("option1")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

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

                entity.Property(e => e.Orderby)
                    .HasColumnName("orderby")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Orderno).HasColumnName("orderno");

                entity.Property(e => e.Prefixcode)
                    .HasColumnName("prefixcode")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rowno)
                    .HasColumnName("rowno")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Shtcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Step)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Suspend).HasColumnName("suspend");
            });

            modelBuilder.Entity<CodestabRel>(entity =>
            {
                entity.HasKey(e => e.KeyId);

                entity.Property(e => e.KeyId)
                    .HasColumnName("KeyID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Accountid)
                    .HasColumnName("ACCOUNTID")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AppTo1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppTo2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppTo3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppTo4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppTo5)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bankid)
                    .HasColumnName("BANKID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Memoaddr).IsUnicode(false);

                entity.Property(e => e.Option1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportTo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Joborder>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("joborder");

                entity.Property(e => e.Copny)
                    .HasColumnName("copny")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Jcode)
                    .IsRequired()
                    .HasColumnName("jcode")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Jdesc)
                    .HasColumnName("jdesc")
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Zcopny)
                    .HasColumnName("zcopny")
                    .HasMaxLength(10)
                    .IsUnicode(false);
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
            modelBuilder.Entity<Stafftab>(entity =>
            {
                entity.ToTable("stafftab");

                entity.Property(e => e.Accountid)
                    .HasColumnName("accountid")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Accountid1)
                    .HasColumnName("accountid1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Acctname)
                    .HasColumnName("acctname")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype)
                    .HasColumnName("acctype")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amtdue)
                    .HasColumnName("amtdue")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Amtduebal)
                    .HasColumnName("amtduebal")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.BSalary)
                    .HasColumnName("bSalary")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Bankcode1)
                    .HasColumnName("bankcode1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bankid)
                    .HasColumnName("bankid")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Bar1).HasColumnName("bar1");

                entity.Property(e => e.BloodGrp)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Bonus)
                    .HasColumnName("bonus")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Branchcode1)
                    .HasColumnName("branchcode1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Btype)
                    .HasColumnName("btype")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Cadre)
                    .HasColumnName("cadre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cardno)
                    .HasColumnName("cardno")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cat2)
                    .HasColumnName("cat2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cc)
                    .HasColumnName("cc")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cesdate)
                    .HasColumnName("cesdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Cessation).HasColumnName("cessation");

                entity.Property(e => e.Children)
                    .HasColumnName("children")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Copny)
                    .HasColumnName("copny")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cstaff).HasColumnName("cstaff");

                entity.Property(e => e.Datebirth)
                    .HasColumnName("datebirth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dayw).HasColumnName("dayw");

                entity.Property(e => e.Deptcode)
                    .HasColumnName("deptcode")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Disable).HasColumnName("disable");

                entity.Property(e => e.Djoin)
                    .HasColumnName("djoin")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dpromo)
                    .HasColumnName("dpromo")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dpromoted)
                    .HasColumnName("dpromoted")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dtengaged)
                    .HasColumnName("dtengaged")
                    .HasColumnType("datetime");

                entity.Property(e => e.Effedate)
                    .HasColumnName("effedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Effewdays)
                    .HasColumnName("effewdays")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.EntryYrExp).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.EyeColor)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fmark).HasColumnName("FMark");

                entity.Property(e => e.Freemethod)
                    .HasColumnName("freemethod")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Function1)
                    .HasColumnName("FUNCTION1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gradelvl)
                    .HasColumnName("gradelvl")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Gynotype)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Housecode)
                    .HasColumnName("housecode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Indu).HasColumnName("indu");

                entity.Property(e => e.Initials)
                    .HasColumnName("initials")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Lastpay)
                    .HasColumnName("lastpay")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Middlename)
                    .HasColumnName("middlename")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Natidno)
                    .HasColumnName("natidno")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Nhf).HasColumnName("nhf");

                entity.Property(e => e.Nsitf1).HasColumnName("nsitf1");

                entity.Property(e => e.Nysccdday)
                    .HasColumnName("nysccdday")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nysccdgroup)
                    .HasColumnName("nysccdgroup")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nysccdno)
                    .HasColumnName("nysccdno")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Office)
                    .HasColumnName("office")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Office2)
                    .HasColumnName("office2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Oldval)
                    .HasColumnName("OLDVAL")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Othernames)
                    .HasColumnName("othernames")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Overtime).HasColumnName("overtime");

                entity.Property(e => e.Ovtcum)
                    .HasColumnName("ovtcum")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Paystatus)
                    .HasColumnName("paystatus")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Paywhen)
                    .HasColumnName("paywhen")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Pdate)
                    .HasColumnName("pdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pendate)
                    .HasColumnName("pendate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Peneffewdays)
                    .HasColumnName("peneffewdays")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Penrate)
                    .HasColumnName("penrate")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Penwdays)
                    .HasColumnName("penwdays")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Percent2).HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Perovt)
                    .HasColumnName("perovt")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Pfa)
                    .HasColumnName("PFA")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Picpath)
                    .HasColumnName("picpath")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Pleave).HasColumnName("pleave");

                entity.Property(e => e.Positionid)
                    .HasColumnName("positionid")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Posting)
                    .HasColumnName("posting")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ppay).HasColumnName("ppay");

                entity.Property(e => e.Relatives)
                    .HasColumnName("relatives")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Rentad).HasColumnName("rentad");

                entity.Property(e => e.Rsapin)
                    .HasColumnName("rsapin")
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Scodee)
                    .HasColumnName("scodee")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Signpic)
                    .HasColumnName("signpic")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Sortcode)
                    .HasColumnName("sortcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sreason)
                    .HasColumnName("sreason")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Staffid)
                    .HasColumnName("staffid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Stafftype)
                    .HasColumnName("stafftype")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.Subaccount)
                    .HasColumnName("subaccount")
                    .HasColumnType("numeric(20, 0)");

                entity.Property(e => e.Suboffice)
                    .HasColumnName("suboffice")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Suspdate)
                    .HasColumnName("suspdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Suspended).HasColumnName("suspended");

                entity.Property(e => e.Suspperm)
                    .HasColumnName("suspperm")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Susstatus)
                    .HasColumnName("susstatus")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Taxable).HasColumnName("taxable");

                entity.Property(e => e.Taxref)
                    .HasColumnName("taxref")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Taxregion)
                    .HasColumnName("taxregion")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tdate)
                    .HasColumnName("tdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Terminate)
                    .HasColumnName("terminate")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Testadmin).HasColumnName("testadmin");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tumbpic)
                    .HasColumnName("tumbpic")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Union1).HasColumnName("union1");

                entity.Property(e => e.Zone)
                    .HasColumnName("zone")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("stock");

                entity.Property(e => e.Acctcode)
                    .HasColumnName("ACCTCODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Assetcode)
                    .HasColumnName("ASSETCODE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Binlocat)
                    .HasColumnName("binlocat")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Copny)
                    .HasColumnName("copny")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cosalesact)
                    .HasColumnName("cosalesact")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Details)
                    .HasColumnName("details")
                    .IsUnicode(false);

                entity.Property(e => e.Expdate)
                    .HasColumnName("expdate")
                    .HasColumnType("date");

                entity.Property(e => e.Groupno)
                    .IsRequired()
                    .HasColumnName("groupno")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Inuse)
                    .HasColumnName("inuse")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inusetime)
                    .HasColumnName("inusetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Itemdesc)
                    .HasColumnName("itemdesc")
                    .IsUnicode(false);

                entity.Property(e => e.Lastcdate)
                    .HasColumnName("LASTCDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lastcprice)
                    .HasColumnName("LASTCPRICE")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Lastcqty)
                    .HasColumnName("LASTCQTY")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinQty)
                    .HasColumnName("min_qty")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Nonestock)
                    .IsRequired()
                    .HasColumnName("nonestock")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')");

                entity.Property(e => e.Partno)
                    .HasColumnName("partno")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Perichitem)
                    .HasColumnName("perichitem")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Placcount)
                    .HasColumnName("placcount")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Project2)
                    .HasColumnName("project2")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.QtyInStock)
                    .HasColumnName("qty_in_stock")
                    .HasColumnType("decimal(16, 2)");

                entity.Property(e => e.ReorderLevel)
                    .HasColumnName("reorder_level")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Reorderqty)
                    .HasColumnName("REORDERQTY")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Salesacct)
                    .HasColumnName("salesacct")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Salesprice)
                    .HasColumnName("salesprice")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.StockStat)
                    .HasColumnName("stock_stat")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Stockno)
                    .IsRequired()
                    .HasColumnName("stockno")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Surdefacct)
                    .HasColumnName("SURDEFACCT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasColumnName("type_code")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Units)
                    .HasColumnName("units")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Uprice)
                    .HasColumnName("uprice")
                    .HasColumnType("decimal(11, 4)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Valmethod)
                    .HasColumnName("valmethod")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Valprice)
                    .HasColumnName("valprice")
                    .HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Vatrate)
                    .HasColumnName("vatrate")
                    .HasColumnType("decimal(8, 2)");

                entity.Property(e => e.WBinFrom)
                    .HasColumnName("w_bin_from")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WBinTo)
                    .HasColumnName("w_bin_to")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WCode)
                    .HasColumnName("w_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Zcopny)
                    .HasColumnName("zcopny")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Accust>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ACCUST");

                entity.Property(e => e.Acaddr)
                    .HasColumnName("ACADDR")
                    .IsUnicode(false);

                entity.Property(e => e.Acaltaddr)
                    .HasColumnName("ACALTADDR")
                    .IsUnicode(false);

                entity.Property(e => e.Acarea)
                    .HasColumnName("ACAREA")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Accdebit)
                    .HasColumnName("ACCDEBIT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Accname)
                    .HasColumnName("ACCNAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Accontr)
                    .HasColumnName("ACCONTR")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Accountid1)
                    .HasColumnName("ACCOUNTID1")
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Acctrac)
                    .HasColumnName("ACCTRAC")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Acctype)
                    .HasColumnName("ACCTYPE")
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Accurbal)
                    .HasColumnName("ACCURBAL")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Accustype)
                    .HasColumnName("ACCUSTYPE")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Acdisc)
                    .HasColumnName("ACDISC")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Acdiscday)
                    .HasColumnName("ACDISCDAY")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Acdueday)
                    .HasColumnName("ACDUEDAY")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Aclimit)
                    .HasColumnName("ACLIMIT")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Acltrdate)
                    .HasColumnName("ACLTRDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Acpbal)
                    .HasColumnName("ACPBAL")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Acprbal)
                    .HasColumnName("ACPRBAL")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Acstate)
                    .HasColumnName("ACSTATE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Actown)
                    .HasColumnName("ACTOWN")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Acvat)
                    .HasColumnName("ACVAT")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Bankcode1)
                    .HasColumnName("BANKCODE1")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Bisline)
                    .HasColumnName("BISLINE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Branchcode1)
                    .HasColumnName("BRANCHCODE1")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Bsortcode)
                    .HasColumnName("BSORTCODE")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Bvnumber)
                    .HasColumnName("bvnumber")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cacregno)
                    .HasColumnName("CACREGNO")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Contact)
                    .HasColumnName("CONTACT")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Contperson)
                    .HasColumnName("contperson")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Copny)
                    .IsRequired()
                    .HasColumnName("COPNY")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Custcode)
                    .IsRequired()
                    .HasColumnName("CUSTCODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Descr)
                    .HasColumnName("descr")
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("FAX")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Invpict)
                    .HasColumnName("INVPICT")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Keyid)
                    .HasColumnName("keyid")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Loccode)
                    .HasColumnName("LOCCODE")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Mdatereg)
                    .HasColumnName("MDATEREG")
                    .HasColumnType("datetime");

                entity.Property(e => e.Memail)
                    .HasColumnName("MEMAIL")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Messcode)
                    .HasColumnName("MESSCODE")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Mphone)
                    .HasColumnName("MPHONE")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MphoneIi)
                    .HasColumnName("mphoneII")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mtype)
                    .HasColumnName("mtype")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Mweb)
                    .HasColumnName("MWEB")
                    .IsUnicode(false);

                entity.Property(e => e.Period)
                    .HasColumnName("PERIOD")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Profess)
                    .HasColumnName("profess")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Taxable)
                    .HasColumnName("TAXABLE")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Typecode)
                    .HasColumnName("TYPECODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zcopny)
                    .HasColumnName("zcopny")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

        }

    }
}
