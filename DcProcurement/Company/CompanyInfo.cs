using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using DcProcurement.JoinTables;
using Microsoft.AspNetCore.Identity;

namespace DcProcurement
{
    public class CompanyInfo
    {
        public int Id { get; set; }
        public string CompanyRegNo { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PostalAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string NatureOfBusiness { get; set; }
        public string Sector { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEstablishment { get; set; }
        public string TIN { get; set; }
        public string ContactName { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public string VendorId { get; set; } //The identity User attached to this company. (User of type vendor)
        public bool Approved { get; set; } //Becomes True after going the workflow stages
        public bool Disqualified { get; set; }

        public DateTime CreationDate { get; set; } 

        public List<PrequalificationJob>  PrequalificationJobs { get; set; }
        public VendorUser Vendor { get; set; }
        public List<CompanyInfoProcurementSubCategory> CompanyInfoSelectedSubcategory { get; set; }
        public List<ExperienceRecord>  ExperienceRecords { get; set; }
        public List<EquipmentDetails> EquipmentDetails { get; set; }
        public List<PersonnelDetails>  PersonnelDetails { get; set; }
        public List<SubmittedCriteria> SubmittedCriterias { get; set; }

    }
}
