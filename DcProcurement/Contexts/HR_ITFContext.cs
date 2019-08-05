using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DcProcurement.Contexts
{
    public partial class HR_ITFContext : DbContext
    {
        public HR_ITFContext()
        {
        }

        public HR_ITFContext(DbContextOptions<HR_ITFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pertab> Pertab { get; set; }
        public virtual DbSet<Stafftab> Stafftab { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=HR_ITF;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pertab>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("pertab");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Birthplace)
                    .HasColumnName("birthplace")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Buddept)
                    .HasColumnName("buddept")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cat)
                    .HasColumnName("cat")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cfmcomment)
                    .HasColumnName("cfmcomment")
                    .HasColumnType("text")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Childno)
                    .HasColumnName("childno")
                    .HasColumnType("numeric(5, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Compcode)
                    .HasColumnName("compcode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Confirmdate)
                    .HasColumnName("confirmdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Confirmduedate)
                    .HasColumnName("confirmduedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Confirmed)
                    .HasColumnName("confirmed")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.ContributePen).HasDefaultValueSql("((0))");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Custcode)
                    .HasColumnName("custcode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Dateconf)
                    .HasColumnName("dateconf")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EmgAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EmgCity)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EmgCountry)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EmgState)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EmgZipCode)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EmpReason)
                    .HasColumnType("text")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EmpType)
                    .HasColumnType("numeric(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Employdate)
                    .HasColumnName("employdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Empplace)
                    .HasColumnName("empplace")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.ExclContrPay).HasDefaultValueSql("((0))");

                entity.Property(e => e.Homecity)
                    .HasColumnName("homecity")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.IndResumeDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.IndStartDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Jobcode)
                    .HasColumnName("jobcode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Jobdesc)
                    .HasColumnName("jobdesc")
                    .HasColumnType("text")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Jobend)
                    .HasColumnName("jobend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Jobstart)
                    .HasColumnName("jobstart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Jobtype)
                    .HasColumnName("jobtype")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Lastfurpay)
                    .HasColumnName("lastfurpay")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Leavebal)
                    .HasColumnName("leavebal")
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Leavedays)
                    .HasColumnName("leavedays")
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lga)
                    .HasColumnName("lga")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Loccode)
                    .HasColumnName("loccode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Married)
                    .HasColumnName("married")
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Office)
                    .HasColumnName("office")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PayAddate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PermAddress)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.PermCity)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.PermCountry)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.PermState)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.PermZipCode)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Phoneno)
                    .HasColumnName("phoneno")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rdate)
                    .HasColumnName("rdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Relateno)
                    .HasColumnName("relateno")
                    .HasColumnType("numeric(5, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReportTime).HasColumnType("datetime");

                entity.Property(e => e.ResCountry)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.ResState)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.ResZipCode)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Rescity)
                    .HasColumnName("rescity")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.ResumeInterdiction).HasDefaultValueSql("((0))");

                entity.Property(e => e.Retired)
                    .HasColumnName("retired")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Retireddate)
                    .HasColumnName("retireddate")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Retiredreason)
                    .HasColumnName("retiredreason")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Section1)
                    .HasColumnName("section1")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Spousedob)
                    .HasColumnName("spousedob")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Spousename)
                    .HasColumnName("spousename")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Spouseno)
                    .HasColumnName("spouseno")
                    .HasColumnType("numeric(5, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Staffid)
                    .HasColumnName("staffid")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Staffpic)
                    .HasColumnName("staffpic")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.StartInterdiction).HasDefaultValueSql("((0))");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.SusTempResumeDate)
                    .HasColumnName("susTempResumeDate")
                    .HasColumnType("date");

                entity.Property(e => e.SusTempStartDate)
                    .HasColumnName("susTempStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.Tribe)
                    .HasColumnName("tribe")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Unioncode)
                    .HasColumnName("unioncode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Stafftab>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("stafftab");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Accountid)
                    .HasColumnName("accountid")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Accountid1)
                    .HasColumnName("accountid1")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Acctype)
                    .HasColumnName("acctype")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Amtdue)
                    .HasColumnName("amtdue")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Amtduebal)
                    .HasColumnName("amtduebal")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BSalary)
                    .HasColumnName("bSalary")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BankAcType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.BankSwitchCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Bankbvn)
                    .HasColumnName("bankbvn")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Bankcode1)
                    .HasColumnName("bankcode1")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Bankid)
                    .HasColumnName("bankid")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Bar1)
                    .HasColumnName("bar1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BloodGrp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Bonus)
                    .HasColumnName("bonus")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Branchcode1)
                    .HasColumnName("branchcode1")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Btype)
                    .HasColumnName("btype")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cadre)
                    .HasColumnName("cadre")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cardno)
                    .HasColumnName("cardno")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cat2)
                    .HasColumnName("cat2")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cc)
                    .HasColumnName("cc")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cesdate)
                    .HasColumnName("cesdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cessation)
                    .HasColumnName("cessation")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Children)
                    .HasColumnName("children")
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Compcode)
                    .HasColumnName("compcode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Cstaff)
                    .HasColumnName("cstaff")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dayw)
                    .HasColumnName("dayw")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Deptcode)
                    .HasColumnName("deptcode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Disable)
                    .HasColumnName("disable")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Djoin)
                    .HasColumnName("djoin")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Dpromo)
                    .HasColumnName("dpromo")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Dpromoted)
                    .HasColumnName("dpromoted")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Dtengaged)
                    .HasColumnName("dtengaged")
                    .HasColumnType("datetime");

                entity.Property(e => e.Effedate)
                    .HasColumnName("effedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Effewdays)
                    .HasColumnName("effewdays")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.EntryYrExp)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ethnicity)
                    .HasColumnName("ethnicity")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Excldfrmbudget).HasColumnName("excldfrmbudget");

                entity.Property(e => e.EyeColor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fed)
                    .HasColumnName("fed")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Fmark)
                    .HasColumnName("FMark")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Freemethod)
                    .HasColumnName("freemethod")
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Gradelvl)
                    .HasColumnName("gradelvl")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Gynotype)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Haircolor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Height)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Housecode)
                    .HasColumnName("housecode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Indu)
                    .HasColumnName("indu")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Initials)
                    .HasColumnName("initials")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Lastpay)
                    .HasColumnName("lastpay")
                    .HasColumnType("numeric(2, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Losdays)
                    .HasColumnName("LOSDays")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Losmonths)
                    .HasColumnName("LOSMonths")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lostotal)
                    .HasColumnName("LOSTotal")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Losyears)
                    .HasColumnName("LOSYears")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Middlename)
                    .HasColumnName("middlename")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Natidno)
                    .HasColumnName("natidno")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Newstaff)
                    .HasColumnName("newstaff")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nhf)
                    .HasColumnName("nhf")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nsitf1)
                    .HasColumnName("nsitf1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nysccdday)
                    .HasColumnName("nysccdday")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Nysccdgroup)
                    .HasColumnName("nysccdgroup")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Nysccdno)
                    .HasColumnName("nysccdno")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Office)
                    .HasColumnName("office")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Oldval)
                    .HasColumnName("OLDVAL")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Othernames)
                    .HasColumnName("othernames")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Overtime)
                    .HasColumnName("overtime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ovtcum)
                    .HasColumnName("ovtcum")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Paystatus)
                    .HasColumnName("paystatus")
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Paywhen)
                    .HasColumnName("paywhen")
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pdate)
                    .HasColumnName("pdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pendate)
                    .HasColumnName("pendate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Peneffewdays)
                    .HasColumnName("peneffewdays")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Penongross).HasDefaultValueSql("((0))");

                entity.Property(e => e.Penrate)
                    .HasColumnName("penrate")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pensionable).HasDefaultValueSql("((0))");

                entity.Property(e => e.Penwdays)
                    .HasColumnName("penwdays")
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Percent2)
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Perovt)
                    .HasColumnName("perovt")
                    .HasColumnType("numeric(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pfa)
                    .HasColumnName("PFA")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Picimg)
                    .HasColumnName("picimg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Picpath)
                    .HasColumnName("picpath")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pictype)
                    .HasColumnName("pictype")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Pleave)
                    .HasColumnName("pleave")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Positionid)
                    .HasColumnName("positionid")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Posting)
                    .HasColumnName("posting")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Ppay)
                    .HasColumnName("ppay")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Promoted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Promoted2).HasDefaultValueSql("((0))");

                entity.Property(e => e.Psal)
                    .HasColumnName("psal")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pwhen).HasColumnName("pwhen");

                entity.Property(e => e.Relatives)
                    .HasColumnName("relatives")
                    .HasColumnType("numeric(3, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rentad)
                    .HasColumnName("rentad")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rsapin)
                    .HasColumnName("rsapin")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Scodee)
                    .HasColumnName("scodee")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Sed)
                    .HasColumnName("sed")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sen)
                    .HasColumnName("sen")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sigimg)
                    .HasColumnName("sigimg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Signpic)
                    .HasColumnName("signpic")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Sigtype)
                    .HasColumnName("sigtype")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Sreason)
                    .HasColumnName("sreason")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Staffid)
                    .HasColumnName("staffid")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Stafftype)
                    .HasColumnName("stafftype")
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Subaccount)
                    .HasColumnName("subaccount")
                    .HasColumnType("numeric(20, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Suboffice)
                    .HasColumnName("suboffice")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Suspdate)
                    .HasColumnName("suspdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Suspended)
                    .HasColumnName("suspended")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Suspperm)
                    .HasColumnName("suspperm")
                    .HasColumnType("numeric(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Susstatus)
                    .HasColumnName("susstatus")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Taxable)
                    .HasColumnName("taxable")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Taxref)
                    .HasColumnName("taxref")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Taxregion)
                    .HasColumnName("taxregion")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Tdate)
                    .HasColumnName("tdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Terminate)
                    .HasColumnName("terminate")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Testadmin)
                    .HasColumnName("testadmin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Thbimg)
                    .HasColumnName("thbimg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Thbtype)
                    .HasColumnName("thbtype")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Tumbpic)
                    .HasColumnName("tumbpic")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.Union1)
                    .HasColumnName("union1")
                    .HasDefaultValueSql("((0))");
            });
        }
    }
}
