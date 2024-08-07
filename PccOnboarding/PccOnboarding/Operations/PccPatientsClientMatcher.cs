using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class PccPatientsClientMatcher : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        LogFile.Write("Matching to PccPatientsClients Table...\n");

        //Gets all the patients from the pccPatientsClientsTable
        var data = await context.Set<PccPatientsClientTable>().ToListAsync();


        foreach (var patient in patientsList)
        {
            var match = data.FirstOrDefault(d =>
                //* took this out because we can just compare the ids and not the names this will be more acurrate 
                // patient.FirstName == d.FirstName &&
                // patient.LastName == d.LastName &&
                // patient.OrgUuid == d.OrgUid &&
                // patient.FacId == d.FacilityId.ToString() &&
                // Convert.ToDateTime(patient.BirthDate).ToString() == Convert.ToDateTime(d.PccDob.ToString()).ToString() &&
                patient.PatientId.ToString() == d.PccId.ToString()
            );
            //* set the matched to true
            if (match != null)
            {
                if (match.SupCareClientId != null)
                {
                    patient.ClientInfoMatched = true;
                    patient.SupCarePatientId = match.SupCareClientId;
                }
                match.FirstName = patient.FirstName;
                match.LastName = patient.LastName;
                patient.PccMatched = true;
            }
        }
        var matches = patientsList.Where(p => p.PccMatched == false).Count();
        LogFile.WriteWithBreak($"Found unmatched to PccPatientsClient Table: {matches,-10}");
        return patientsList;

    }
}
