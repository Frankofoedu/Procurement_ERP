using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DcProcurement.Contexts
{
    [Table("uom")]
    public partial class UnitOfMeasurement
    {
        public string Ucode { get; set; }
        public string Uname { get; set; }
        public string Copny { get; set; }
        public string Zcopny { get; set; }
        public int Id { get; set; }
    }
}
