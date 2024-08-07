using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PccOnboarding.Models.Tables;

[Table("tbl_cl_active")]
public class ClientActiveTable
{
    [Key]
    [Column("cl_active_id")]
    public int? ClientActiveTableId { get; set; }
    [Column("cl_id")]
    public int? SupCareClientId { get; set; }
    [Column("svce_type_id")]
    public int? ServiceType { get; set; }
    [Column("cl_active_dte")]
    public DateTime? AdmissionDate { get; set; }
    [Column("cl_term_type_id")]
    public int? TerminationType { get; set; }
    [Column("cl_active_end_dte")]
    public DateTime? DischargeDate { get; set; }
    [Column("facility_id")]
    public int? SupCareFacId { get; set; }
    [Column("room_num")]
    public string? Room { get; set; }
    [Column("bed_num")]
    public string? Bed { get; set; }
    [Column("flr_num")]
    public string? Floor { get; set; }
}
