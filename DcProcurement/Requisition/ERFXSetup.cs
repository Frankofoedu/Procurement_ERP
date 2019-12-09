using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static DcProcurement.Enums;

namespace DcProcurement
{
    public class ERFXSetup
    {
        private ERFXSetup()
        {

        }

        /// <summary>
        ///     This constructor assigns the id of the new object a value.
        /// </summary>
        /// <param name="id">Id value should be the Requisition ID</param>
        public ERFXSetup(int id)
        {
            Id = id;
        }

        [ForeignKey("Requisition")]
        public int Id { get; private set; }
        [NotMapped]
        public string ErfxNum { get { return Id.ToString("0000"); } }
        public DateTime? ErfxDate { get; set; }
        public string ProjectTitle { get; set; }
        public BidTypes BidType { get; set; }

        public Requisition Requisition { get; set; }

        public TechnicalERFXSetup TechnicalERFXSetup { get; set; }
        public FinancialERFXSetup FinancialERFXSetup { get; set; }
    }
    public class TechnicalERFXSetup
    {
        [ForeignKey("ERFXSetup")]
        public int Id { get; private set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }

        public ERFXSetup ERFXSetup { get; set; }
    }

    public class FinancialERFXSetup
    {
        [ForeignKey("ERFXSetup")]
        public int Id { get; private set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }

        public ERFXSetup ERFXSetup { get; set; }
    }
}
