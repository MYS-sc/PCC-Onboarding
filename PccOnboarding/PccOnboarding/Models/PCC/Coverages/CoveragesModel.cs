namespace PccOnboarding.Models.PCC.Coverages;

public class CoveragesModel
{
    public int? CoveragesId { get; set; }
    public string? EffectiveFromDateTime { get; set; }
    public string? EffectiveToDateTime { get; set; }
    public int PatientId { get; set; }
    public List<PayersModel> Payers { get; set; }
}
