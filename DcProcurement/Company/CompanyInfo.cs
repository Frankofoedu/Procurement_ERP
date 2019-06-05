using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using DcProcurement.JoinTables;
using Microsoft.AspNetCore.Identity;

namespace DcProcurement
{
    public class CompanyInfo : IdentityUser
    {
        public string CompanyRegNo { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PostalAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string NatureOfBusiness { get; set; }
        public string Sector { get; set; }
        public DateTime DateEstablishment { get; set; }
        public string TIN { get; set; }
        public string ContactName { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }

        public List<CompanyInfoProcurementSubCategory> CompanyInfos { get; set; }
        public List<ExperienceRecord>  ExperienceRecords { get; set; }
        public List<EquipmentDetails> EquipmentDetails { get; set; }
        public List<PersonnelDetails>  PersonnelDetails { get; set; }
        public List<SubmittedCriteria> SubmittedCriterias { get; set; }

    }
}
