using Microsoft.EntityFrameworkCore;
using PccOnboarding.models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Steps;

public class PccPatientsClientMatcher : IOperation
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        LogFile.Write("Matching to PccPatientsClients Table...\n");

        //Gets all the patients from the pccPatientsClientsTable
        var data = context.Set<PccPatientsClientTable>().ToList();
        // This code block uses LINQ to filter the patientsList based on the absence of matching records in the data list.
        // It checks each patient in patientsList against each record in data to see if they have the same FirstName, LastName, OrgUuid, FacId, BirthDate, and PatientId.
        // If a match is not found, the patient is added to the unmatchedData list.
        foreach (var patient in patientsList)
        {
            var match = data.FirstOrDefault(d =>
                patient.FirstName == d.FirstName &&
                patient.LastName == d.LastName &&
                patient.OrgUuid == d.OrgUid &&
                patient.FacId == d.FacilityId.ToString() &&
                Convert.ToDateTime(patient.BirthDate).ToString() == Convert.ToDateTime(d.PccDob.ToString()).ToString() &&
                patient.PatientId.ToString() == d.PccId.ToString()
            );
            if (match != null)
            {
                patient.PccMatched = true;
            }
        }
        var matches = patientsList.Where(p => p.PccMatched == false).Count();
        LogFile.WriteWithBreak($"Found unmatched to PccPatientsClient Table: {matches,-10}");
        return patientsList;

    }
}
