using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;

namespace PccOnboarding.Operations;

public interface IDataGetter
{
    Task<List<OurPatientModel>> Execute(string orgId, int facId, int? ourFacId, string state);
}
