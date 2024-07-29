using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class ClientInfoMatchedPccPatientsClientAdder : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        //* get from the patientlist only the onse that have been match clientInfoTable and not pccPatientsClientsTable
        var matched = patientsList.Where(p => p.ClientInfoMatched == true && !p.PccMatched);
        //* if there is nothing to add we return
        if (matched.Count() == 0)
        {
            LogFile.WriteWithBreak("No ClientInfo Matched patients to add to PccPatientsClientsTable");
            return patientsList;
        }
        LogFile.Write("Adding ClientInfo Matched patients To PccPatientsClientsTable...\n");
        foreach (var match in matched)
        {
            var pccClient = new PccPatientsClientTable()
            {
                FirstName = match.FirstName,
                LastName = match.LastName,
                PccDob = Convert.ToDateTime(match.BirthDate),
                ClientId = match.OurPatientId,
                OrgUid = match.OrgUuid,
                PccId = match.PatientId,
                FacilityId = int.Parse(match.FacId)
            };
            context?.Set<PccPatientsClientTable>().AddAsync(pccClient);
            match.PccMatched = true;
            LogFile.Write($"Added PccPatientsClients - FirstName: {match.FirstName,-15} LastName: {match.LastName,-15} Id:{match.OurPatientId,-10} pccId:{match.PatientId,-10}");
        }
        await context?.SaveChangesAsync();
        LogFile.WriteWithBreak("Done Adding To PccPatientsClientsTable");
        return patientsList;
    }
}
