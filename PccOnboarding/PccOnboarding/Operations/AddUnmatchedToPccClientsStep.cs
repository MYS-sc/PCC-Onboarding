using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;
using PccWebhook.Utils;

namespace PccOnboarding.Operations;

public class AddUnmatchedToPccClientsStep : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        //* Get all the patients that are not matched to pcc and not matched to clietsinfotable
        var unmatched = patientsList.Where(p => p.ClientInfoMatched == false && p.PccMatched == false && p.IsSimilar == false);
        //* If there are no unmatched patients we are done and return
        if (unmatched.Count() == 0)
        {
            LogFile.WriteWithBreak("No patients to add to PccPatientsClientsTable\n");
            return patientsList;
        }
        //Console.WriteLine($"unmatched {unmatched.Count()}");
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
                FacilityId = int.Parse(match.FacId),
                //!Make sure to take this out in production !!!
                IsTestClient = true

            };
            context?.Set<PccPatientsClientTable>().AddAsync(pccClient);


            LogFile.Write($"Added PccPatientsClients - FirstName: {match.FirstName,-15} LastName: {match.LastName,-15} Id:{match.OurPatientId,-10} pccId:{match.PatientId,-10}");

        }
        await context?.SaveChangesAsync();
        LogFile.WriteWithBreak("Done Adding To PccPatientsClientsTable");
        return patientsList;
    }
}
