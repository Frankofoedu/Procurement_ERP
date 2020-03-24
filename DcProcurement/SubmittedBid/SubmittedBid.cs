using System;
using System.Collections.Generic;
using System.Text;
using static DcProcurement.Enums;

namespace DcProcurement
{
    public class SubmittedBid
    {
        public int Id { get; set; }

        public int ERFXSetupId { get; set; }
        public bool TermsAndConditions { get; set; }

        #region Confirmation
        public string ConfirmerName { get; set; }
        public string ConfirmerEmail { get; set; }
        public string ConfirmerPhone { get; set; }

        #endregion

        #region Declaration
        public string DeclarerName { get; set; }
        public string DeclarerEmail { get; set; }
        public string DeclarerPhone { get; set; }
        #endregion

        #region OrganizationInfo
        public string OrganizationName { get; set; }
        public string BrokerName { get; set; }
        public string PreferredCurrency { get; set; }
        public string NatureOfBusiness { get; set; }
        public string Logo { get; set; }
        #endregion

        #region FillerDetails
        public string FillerTitle { get; set; }
        public string FillerLastName { get; set; }
        public string FillerFirstName { get; set; }
        public string FillerOtherName { get; set; }
        public string FillerJobTitle { get; set; }
        public string FillerDepartment { get; set; }
        #endregion

        #region Address
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        #endregion

        #region Contact
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string AccessLevel { get; set; }
        #endregion

        public SubmittedBidState SubmittedBidState { get; set; }

        public List<BidProcessAttachment> BidProcessAttachments { get; }
        public List<SubmittedFinancialBid> SubmittedFinancialBids { get; }
        public List<SubmittedTechnicalBid> SubmittedTechnicalBids { get; }
    }
}
