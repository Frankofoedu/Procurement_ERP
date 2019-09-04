using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
