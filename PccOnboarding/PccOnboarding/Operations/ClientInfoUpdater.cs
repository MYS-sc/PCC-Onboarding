using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.Tables;

namespace PccOnboarding.Operations;

public class ClientInfoUpdater : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientList, DbContext context)
    {
        var matched = patientList.Where(p => p.ClientInfoMatched == true).ToList();
        //var notMatched = patientList.Where(p => p.ClientInfoMatched == false).ToList();
        foreach (var patient in matched)
        {
            if (patient.SupCarePatientId != null)
            {
                var client = await context.Set<ClientInfoTable>().FirstAsync(c => c.SupCareCleintId == patient.SupCarePatientId);
                client.FacilityId = patient.SupCareFacId;
            }
        }
        await context.SaveChangesAsync();
        return patientList;
    }
}
