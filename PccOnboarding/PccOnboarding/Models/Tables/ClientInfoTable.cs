using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PccOnboarding.Models.Tables
{
    [Table("ClientInfoTable")]
    public class ClientInfoTable
    {
        [Key]
        [Column("ClientId")]
        public int? ClientId { get; set; }
        [Column("Medicare #")]
        public string? MedicareNum { get; set; }
        [Column("ClientStatus")]
        public string? ClientStatus { get; set; }
        [Column("LastName")]
        public string? LastName { get; set; }
        [Column("FirstName")]
        public string? OurFirstName { get; set; }
        [Column("DateOfbirth")]
        public DateTime? DateOfBirth { get; set; }
        [Column("MedIcaid/Insurance #")]
        public string? MedicaidInsuranceNum { get; set; }
        [Column("Address")]
        public string? Address { get; set; }
        [Column("City")]
        public string? City { get; set; }
        [Column("State")]
        public string? State { get; set; }
        [Column("Zip")]
        public string? Zip { get; set; }
        [Column("Coverage")]
        public string? Coverage { get; set; }
        [Column("Memo")]
        public string? Memo { get; set; }
        [Column("Diagnosis1")]
        public string? Diagnosis1 { get; set; }
        [Column("Diagnosis2")]
        public string? Diagnosis2 { get; set; }
        [Column("Diagnosis3")]
        public string? Diagnosis3 { get; set; }
        [Column("Attachment")]
        public string? Attachment { get; set; }
        [Column("NonPaying")]
        public bool? NonPaying { get; set; }
        [Column("Status")]
        public decimal? Status { get; set; }
        [Column("DateChecked")]
        public DateTime? DateChecked { get; set; }
        [Column("YearDeductible")]
        public int? YearDeductible { get; set; }
        [Column("Consent")]
        public bool? Consent { get; set; }
        [Column("DocOrder")]
        public bool? DocOrder { get; set; }
        [Column("facility_id")]
        public int? FacilityId { get; set; }
        [Column("gender")]
        public string? Gender { get; set; }
        [Column("marital_st")]
        public string? MaritalSatus { get; set; }
        [Column("insurance_num")]
        public string? InsuranceNum { get; set; }
        [Column("mCare")]
        public int? Medicare { get; set; }
        [Column("mCaid")]
        public int? Medicaid { get; set; }
        [Column("Ins")]
        public int? Insurance { get; set; }
        [Column("Ins_2")]
        public int? Insurance2 { get; set; }
        [Column("Insurance_2_num")]
        public string? Insurance2Num { get; set; }
        [Column("auth_appv")]
        public bool? AuthApproved { get; set; }
        [Column("auth_num")]
        public string? AuthNum { get; set; }
        [Column("auth_st_dte")]
        public DateTime? AuthStartDate { get; set; }
        [Column("auth_end_dte")]
        public DateTime? AuthEndDate { get; set; }
        [Column("auth_ses")]
        public int? AuthSes { get; set; }
        [Column("inActive")]
        public bool? Inactive { get; set; }
        [Column("test_client")]
        public bool? TestClient { get; set; }
        [Column("primary_covered")]
        public bool? PrimaryCovered { get; set; }
        [Column("pCare")]
        public bool? PCare { get; set; }
        [Column("pCaid")]
        public bool? PCaid { get; set; }
        [Column("primary_auth")]
        public bool? PrimaryAuth { get; set; }
        [Column("care_a")]
        public bool? CareA { get; set; }
        [Column("ins_effective_dte")]
        public DateTime? InsutanceEffectiveDate { get; set; }
        //[Column("SSMA_TimeStamp")]
        //public TimestampAttribute? SSMATimeStamp { get; set; }
        [Column("ins_cr_dte")]
        public DateTime? InsuranceCreatedDate { get; set; }
        [Column("cl_no_90791")]
        public int? ClientNo90791 { get; set; }
    }
}

