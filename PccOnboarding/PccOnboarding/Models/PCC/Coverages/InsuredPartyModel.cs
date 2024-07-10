using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PccOnboarding;

public class InsuredPartyModel
{
    public string? BirthDate { get; set; }
    public string? FirstName { get; set; }
    public string? Gender { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Relationship { get; set; }
    public string? SocialBeneficiaryIdentifier { get; set; }
}
