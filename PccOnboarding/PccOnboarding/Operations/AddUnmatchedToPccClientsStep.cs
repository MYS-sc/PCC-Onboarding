using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;
using PccWebhook.Utils;

namespace PccOnboarding.Operations;

public class AddUnmatchedToPccClientsStep : IOperation
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        var unmatched = patientsList.Where(p => p.ClientInfoMatched == false && p.PccMatched == false);
        if (unmatched.Count() == 0)
        {
            LogFile.WriteWithBreak("No patients to add to PccPatientsClientsTable\n");
            return patientsList;
        }
        Console.WriteLine($"unmatched {unmatched.Count()}");
        LogFile.Write("Adding To PccPatientsClientsTable...\n");

        foreach (var match in unmatched)
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
            context?.Set<PccPatientsClientTable>().Add(pccClient);


            LogFile.Write($"Added PccPatientsClients - FirstName: {match.FirstName,-15} LastName: {match.LastName,-15} Id:{match.OurPatientId,-10} pccId:{match.PatientId,-10}");

        }
        context?.SaveChanges();
        LogFile.BreakLine();

        return patientsList;
    }
}
