using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PccOnboarding;

[Table("tbl_pcc_fac")]
public class PccFacilitiesTable
{
    [Key]
    [Column("fac_id")]
    public int? FacilityId { get; set; }
    [Column("pcc_orgUid")]
    public string? OrgUuid { get; set; }
    [Column("pcc_facID")]
    public int? PccId { get; set; }

}
