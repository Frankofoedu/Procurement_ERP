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
        public const string ProcurementWorkflow = "Procurement";
        public const int ProcurementWorkflowId = 3;
        public const string ItemPricingWorkflow = "ItemPricing";
        public const int ItemPricingWorkflowId = 4;


        #region Constants for procurement workflow Actions
        public const string ProcurementCosting = "Procurement Costing";
        public const int ProcurementCostingId = 100;
        public const string BudgetaryControl = "Budgetary Control";
        public const int BudgetaryControlId = 200;
        public const int ApproverId = 300;
        public const string Approver = "Procurement Approval";
        public const int AuthorizerId = 400;
        public const string Authorizer = "Procurement Authorization";
        public const int ApproveRaiseERFxId = 500;
        public const string ApproveRaiseERFx = "Approval to Raise eRFx";
        public const int RaiseERFxId	 = 600;
        public const string RaiseERFx = "Raise eRFx";
        #endregion


        public static class Role
        {
            public const string Admin = "Admin";
            public const string Staff = "Staff";
            public const string Vendor = "Vendor";
        }
    }

}
