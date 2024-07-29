using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

namespace PccOnboarding;

[Table("OnboardingSimilarLogs")]
public class OnboardingSimilarLogsTable
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("orgUuid")]
    public string? OrgUuid { get; set; }
    [Column("pccFacId")]
    public string? PccFacId { get; set; }
    [Column("pccId")]
    public int PccId { get; set; }
    [Column("firstName")]
    public string? FirstName { get; set; }
    [Column("lastName")]
    public string? LastName { get; set; }
    [Column("dateOfBirth")]
    public string? DateOfBirth { get; set; }
    [Column("ourId")]
    public int? OurId { get; set; }
    [Column("ourFacId")]
    public int? OurFacId { get; set; }
    [Column("state")]
    public string State { get; set; }
    [Column("reviewed")]
    public bool? Reviewed { get; set; } = false;
    [Column("similarPatientId")]
    public int? SimilarPatientId { get; set; }
}
