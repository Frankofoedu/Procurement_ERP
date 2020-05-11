using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class CodestabRel
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal KeyId { get; set; }
        /// <summary>
        /// contains the division or deparetment code
        /// </summary>
        public string Code { get; set; }
        public string Option1 { get; set; }
        /// <summary>
        /// zone that user logged in from 
        /// </summary>
        public string AppTo1 { get; set; }
        /// <summary>
        /// office that user logged in from 
        /// </summary>
        public string AppTo2 { get; set; }
        /// <summary>
        /// main department of the user
        /// </summary>
        public string AppTo3 { get; set; }
        /// <summary>
        /// department of the user
        /// </summary>
        public string AppTo4 { get; set; }
        /// <summary>
        /// section of the user
        /// </summary>
        public string AppTo5 { get; set; }
        /// <summary>
        /// code to get who was setup as the head
        /// </summary>
        public string ReportTo { get; set; }
        public string CompCode { get; set; }
        public string Memoaddr { get; set; }
        public string Bankid { get; set; }
        public string Accountid { get; set; }
    }
}
