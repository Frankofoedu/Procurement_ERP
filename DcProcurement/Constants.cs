using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class Constants
    {
        #region Constants for workflow Types
        public const string RequisitionWorkflow = "Requisition";
        public const int RequisitionWorkflowId = 1;
        public const string PrequalificationWorkFlow = "Prequalification";
        public const int PrequalificationWorkFlowId = 2;
        public const string ProcurementWorkflow = "Procurement";
        public const int ProcurementWorkflowId = 3;
        public const string ItemPricingWorkflow = "ItemPricing";
        public const int ItemPricingWorkflowId = 4;
        #endregion

        #region Constants for procurement workflow Actions
        public const string ProcurementCostingAction = "Procurement Costing";
        public const int ProcurementCostingActionId = 100;
        public const string BudgetaryControlAction = "Budgetary Control";
        public const int BudgetaryControlActionId = 200;
        public const int ProcurementApproverActionId = 300;
        public const string ProcurementApproverAction = "Procurement Approval";
        public const int AuthorizerActionId = 400;
        public const string AuthorizerAction = "Procurement Authorization";
        public const int ApproveRaiseERFxActionId = 500;
        public const string ApproveRaiseERFxAction = "Approval to Raise eRFx";
        public const int RaiseERFxActionId	 = 600;
        public const string RaiseERFxAction = "Raise eRFx";
        #endregion

        #region Constants for Other Important Workflow Action
        public const string InitiatorActionName = "Initiator";
        public const int InitiatorActionId = 700;
        public const string ApproverActionName = "Approval";
        public const int ApproverActionId = 800;
        #endregion

        public static class Role
        {
            public const string Admin = "Admin";
            public const string Staff = "Staff";
            public const string Vendor = "Vendor";
        }

        #region Admin User
        public const string AdminEmail = "AdminEproc";
        public const string AdminUserName = "AdminEproc";
        public const string AdminPassword = "oj5!%hs17";
        public const string AdminId = "46b5ea46-80ab-4fba-8507-4908ac269d00";
        #endregion
    }

}
