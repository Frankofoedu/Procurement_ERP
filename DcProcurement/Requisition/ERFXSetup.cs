using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DcProcurement
{
    class ERFXSetup
    {
        private ERFXSetup()
        {

        }

        /// <summary>
        ///     This constructor assigns the id of the new object a value.
        /// </summary>
        /// <param name="id">Id  value should be the last id in the DB + 1</param>
        public ERFXSetup(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        [NotMapped]
        public string ErfxNum { get { return Id.ToString("0000"); } }
        public DateTime? ErfxDate { get; set; }
        public string ProjectTitle { get; set; }
        public string BidType { get; set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }
    }
}
