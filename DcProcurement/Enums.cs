using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public static class Enums
    {
        public enum VerificationStates { Unchecked, Invalid, Valid};

        public enum BidTypes { Technical, Financial, Both };
        public enum JobState { Done, Cancelled, NotDone };
        public enum SubmittedBidState { Saved, Submitted };
        public enum WorkflowStaffState { Normal, Suspended };
        public enum RequisitionState { Saved, Submitted, Approved, Quarantined };
        public enum ProcurementState { NotStarted, Started, Priced, BudgetCleared };
    }
}
