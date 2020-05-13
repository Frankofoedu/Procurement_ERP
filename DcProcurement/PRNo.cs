using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DcProcurement
{
    public class PRNo
    {
        public int Id { get; set; }
        public string CompCode { get; set; }
        public string  DeptCode { get; set; }
        public string DeptPrefix { get; set; }
        public string Year { get; set; }
        public string SerialNo { get; set; }
        [NotMapped]
        public string RequisitionCode { get { return $"{CompCode}/{DeptCode}/{DeptPrefix}/{Year}/{SerialNo}"; } }
    }
}
