using PccOnboarding.Models.PCC;

namespace PccOnboarding.Models.Our;

public class OurPatientModel : PccPatientsModel
{
    public bool IsSimilar { get; set; }
    public int? SimilarPatientId { get; set; }
    public int? OurFacId { get; set; }
    public int? OurPatientId { get; set; }
    public bool PccMatched { get; set; } = false;
    public bool ClientInfoMatched { get; set; } = false;
    public string State { get; set; }
}
