using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DcProcurement
{
    public class CompanyInfo
    {
        public int Id { get; set; }
        public string CompanyRegNo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PostalAddress { get; set; }
        public string FaxNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string NatureOfBusiness { get; set; }
        public string OwnershipStructure { get; set; }
        public string ParentCompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public string Sector { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEstablishment { get; set; }
        public string TIN { get; set; }
        public string ContactName { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public bool HasHSE { get; set; }
        public bool HasNCEC { get; set; }
        public string  CurrentTurnover { get; set; }
        public string CurrentNetProfit { get; set; }
        public string CurrentNetAsset { get; set; }
        public string NumberOfEmployees { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string BankSortCode { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string BVN { get; set; }
        public string AuthorizedName { get; set; }
        public string AuthorizedDesignation { get; set; }
        public DateTime AuthorizedDate { get; set; }

        public Attachment CompanyProfile { get; set; }
        public Attachment CertificateOfIncorp { get; set; }
        public Attachment TaxRegistration { get; set; }

        /// <summary>
        /// The identity User attached to this company. (User of type vendor)
        /// </summary>
        public string VendorId { get; set; }
        /// <summary>
        /// Becomes True after going the workflow stages
        /// </summary>
        public bool Approved { get; set; } 
        public bool Disqualified { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public List<PrequalificationJob>  PrequalificationJobs { get; set; }
        public VendorUser Vendor { get; set; }
        public List<CompanyInfoProcurementSubCategory> CompanyInfoSelectedSubcategory { get; set; }
        public List<ExperienceRecord>  ExperienceRecords { get; set; }
        public List<EquipmentDetails> EquipmentDetails { get; set; }
        public List<PersonnelDetails>  PersonnelDetails { get; set; }
        public List<SubmittedCriteria> SubmittedCriterias { get; set; }

        /*
         difference between agency code and identification no
         
         */

    }
}
