using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PccOnboarding;

[Table("tbl_facility")]
public class FacilitiesTable
{
    [Key]
    [Column("facility_id")]
    public string? FacilityId { get; set; }
    [Column("facility_name")]
    public string? FacilityName { get; set; }
    [Column("facility_street")]
    public string? FacilityStreet { get; set; }
    [Column("facility_city")]
    public string? FacilityCity { get; set; }
    [Column("facility_state")]
    public string? FacilityState { get; set; }
    [Column("facility_zip")]
    public string? FacilityZip { get; set; }
    [Column("Facility_code")]
    public string? FacilityCode { get; set; }
}


