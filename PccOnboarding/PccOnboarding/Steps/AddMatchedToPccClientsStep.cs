using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Steps;

public class AddMatchedToPccClientsStep
{
    public IEnumerable<PatientsModel> Execute(IEnumerable<PatientsModel> patientsList, Type db, int? facId)
    {
        LogFile.Write("Adding To PccPatientsClientsTable...\n");
        var matched = patientsList.Where(p => p.ourId != null);
        using (var context = (DbContext)Activator.CreateInstance(db))
        {
            foreach (var match in matched)
            {
                var pccClient = new PccPatientsClientTable()
                {
                    FirstName = match.FirstName,
                    LastName = match.LastName,
                    PccDob = Convert.ToDateTime(match.BirthDate),
                    ClientId = match.ourId,
                    OrgUid = match.OrgUuid,
                    PccId = match.PatientId,
                    FacilityId = int.Parse(match.FacId)

                };
                context?.Set<PccPatientsClientTable>().Add(pccClient);
                context?.SaveChanges();
                //Console.WriteLine(pccClient.PccClientId);
                LogFile.Write($"Added PccPatientsClients - FirstName: {match.FirstName,-15} LastName: {match.LastName,-15} Id:{match.ourId,-10} pccId:{match.PatientId,-10}");
            }
            LogFile.BreakLine();

        }

        return patientsList;
    }
}
