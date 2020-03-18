using Microsoft.AspNetCore.Http;

namespace BsslProcurement.ViewModels
{
    public class GeneralInformationPartialViewModel
    {
        public string OrganizationName { get; set; }
        public string BrokerName { get; set; }
        public string PreferredCurrency { get; set; }
        public string NatureOfBusiness { get; set; }
        public IFormFile LogoFile { get; set; }

        public string FillerTitle { get; set; }
        public string FillerLastName { get; set; }
        public string FillerFirstName { get; set; }
        public string FillerOtherName { get; set; }
        public string FillerJobTitle { get; set; }
        public string FillerDepartment { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string AccessLevel { get; set; }
    }
}