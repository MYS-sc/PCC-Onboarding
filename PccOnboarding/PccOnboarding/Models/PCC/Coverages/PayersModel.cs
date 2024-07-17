namespace PccOnboarding.Models.PCC.Coverages;

public class PayersModel
{
    public string? AccountDescription { get; set; }
    public string? AccountNumber { get; set; }
    public IssuerModel? Issuer { get; set; }
    public InsuredPartyModel? InsuredParty { get; set; }
    public string? PayPlanType { get; set; }
    public string? PayPlanTypeCode { get; set; }
    public string? PayerCode { get; set; }
    public string? PayerCode2 { get; set; }
    public int? PayerId { get; set; }
    public string? PayerName { get; set; }
    public string? PayerRank { get; set; }
    public string? PayerType { get; set; }
    public bool? Pps { get; set; }
}
