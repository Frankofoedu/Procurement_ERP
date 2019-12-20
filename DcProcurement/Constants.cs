using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class Constants
    {
        public const string RequisitionWorkflow = "Requisition";
        public const int RequisitionWorkflowId = 1;
        public const string PrequalificationWorkFlow = "Prequalification";
        public const int PrequalificationWorkFlowId = 2;
        public const string ItemPricingWorkflow = "Procurement";
        public const int ItemPricingWorkflowId = 3;


        public static class Role
        {
            public const string Admin = "Admin";
            public const string Staff = "Staff";
            public const string Vendor = "Vendor";
        }
    }

}
