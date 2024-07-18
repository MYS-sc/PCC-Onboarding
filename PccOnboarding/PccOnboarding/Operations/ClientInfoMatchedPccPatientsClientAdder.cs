using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class ClientInfoMatchedPccPatientsClientAdder : IOperation
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {

        var matched = patientsList.Where(p => p.ClientInfoMatched == true && !p.PccMatched);
        if (matched.Count() == 0)
        {
            LogFile.WriteWithBreak("No patients to add to PccPatientsClientsTable\n");
            return patientsList;
        }
        LogFile.Write("Adding To PccPatientsClientsTable...\n");
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
            context?.Set<PccPatientsClientTable>().Add(pccClient);
            match.PccMatched = true;
            LogFile.Write($"Added PccPatientsClients - FirstName: {match.FirstName,-15} LastName: {match.LastName,-15} Id:{match.OurPatientId,-10} pccId:{match.PatientId,-10}");
        }
        context?.SaveChanges();
        LogFile.BreakLine();



        return patientsList;
    }
}
