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

        public IFormFile CompanyProfileFile { get; set; }
        public IFormFile CertificateOfIncorporationFile { get; set; }
        public IFormFile TaxRegistrationCertificateFile { get; set; }
        public IFormFile CurrentTaxCertificateFile { get; set; }
        public IFormFile ProcurementRegistrationFile { get; set; }
        public IFormFile NSITFComplianceCertificateFile { get; set; }
        public IFormFile PENCOMComplianceCertificateFile { get; set; }
        public IFormFile ITFComplianceCertificateFile { get; set; }
        public IFormFile BankGuarantyFile { get; set; }
        public IFormFile AuditedFinancialStatementsFile { get; set; }
    }
}