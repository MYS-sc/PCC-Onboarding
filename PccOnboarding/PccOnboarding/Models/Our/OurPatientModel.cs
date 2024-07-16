using PccOnboarding.Models.PCC;

namespace PccOnboarding.models.Our;

public class OurPatientModel : PccPatientsModel
{
    public bool IsNewClient { get; set; } = false;
    public int? OurFacId { get; set; }
    public int? OurPatientId { get; set; }
    public bool PccMatched { get; set; } = false;
    public bool ClientInfoMatched { get; set; } = false;
    public string State { get; set; }
}
