namespace PccOnboarding.Models.PCC.Coverages;

public class IssuerModel
{
    public string? ContactName { get; set; }
    public string? Group { get; set; }
    public IssuerAddressModel? IssuerAddress { get; set; }
    public int? IssuerId { get; set; }
    public string? IssuerName { get; set; }
    public string? Phone { get; set; }
    public string? PlanEffactiveDate { get; set; }
    public string? PlanExpirationDate { get; set; }
    public string? PlanNumber { get; set; }
    public string? ProviderNumber { get; set; }
}
