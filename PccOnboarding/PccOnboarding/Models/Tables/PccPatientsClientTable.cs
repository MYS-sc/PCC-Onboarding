using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PccOnboarding.Models.Tables
{
    [Table("tbl_pcc_patients_client")]
    public class PccPatientsClientTable
    {
        [Key]
        [Column("pcc_client_id")]
        public int? PccClientId { get; set; }
        [Column("cl_id")]
        public int? SupCareClientId { get; set; }
        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }
        [Column("pcc_id")]
        public int? PccId { get; set; }
        [Column("facID")]
        public int? FacilityId { get; set; }
        [Column("orgUuid")]
        public string? OrgUid { get; set; }
        [Column("pcc_na")]
        public int? PccNA { get; set; }
        [Column("pcc_dob")]
        public DateTime? PccDob { get; set; }
        //!Make sure to take out in production THIS IS FOR TESTING ONLY
        [Column("testClient")]
        public bool? IsTestClient { get; set; }
        //!Make sure to take out in production THIS IS FOR TESTING ONLY
        [Column("testUpdate")]
        public bool? TestUpdate { get; set; }
    }
}
