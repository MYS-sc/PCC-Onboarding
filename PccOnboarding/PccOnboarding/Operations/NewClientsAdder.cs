using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class NewClientsAdder : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        //* Only get the ones that don't match the clientinfotable from the patentlist
        var matched = patientsList.Where(p => !p.ClientInfoMatched && p.IsSimilar == false).ToList();
        //* If there is nothing to add we return
        if (matched.Count() == 0)
        {
            LogFile.WriteWithBreak("No patients to add to ClientsInfoTable");
            return patientsList;
        }
        LogFile.Write("Adding new Clients...\n");
        foreach (var nm in matched)
        {
            var patient = new ClientInfoTable
            {
                SupCareFirstName = nm.FirstName?.ToLower(),
                SupCareLastName = nm.LastName?.ToLower(),
                DateOfBirth = nm.BirthDate == null ? null : Convert.ToDateTime(nm.BirthDate),
                Gender = nm.Gender,
                FacilityId = nm.SupCareFacId,
                MaritalSatus = nm.MaritalStatus,
                //!test only take out in production
                Diagnosis3 = "Test_MY"


            };
            var table = await context.Set<ClientInfoTable>().AddAsync(patient);
            //* need to save changes to get the new id that was just assigned By the database
            await context.SaveChangesAsync();
            //* get the new id that was just assigned to the new clientinfo input aka ourIdma
            nm.SupCarePatientId = patient.SupCareCleintId;

            LogFile.Write($"Added To: ClientsInfoTable OurId: {patient.SupCareCleintId,-10} FirstName: {patient.SupCareFirstName,-15} LastName: {patient.SupCareLastName,-10} FacilityId: {patient.FacilityId,-10}");
        }

        LogFile.BreakLine();


        return patientsList;
    }
}
