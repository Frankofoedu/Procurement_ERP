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
    }
}
