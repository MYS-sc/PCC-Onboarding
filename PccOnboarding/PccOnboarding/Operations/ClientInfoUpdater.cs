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
            var client = context.Set<ClientInfoTable>().First(c => c.ClientId == patient.OurPatientId);
            client.FacilityId = patient.OurFacId;
        }
        await context.SaveChangesAsync();
        return patientList;
    }
}
