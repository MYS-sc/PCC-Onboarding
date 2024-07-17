using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class NewClientsAdder : IOperation
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        LogFile.Write("Adding new Clients...\n");

        foreach (var nm in patientsList.Where(p => !p.ClientInfoMatched))
        {


            var patient = new ClientInfoTable
            {
                OurFirstName = nm.FirstName,
                LastName = nm.LastName,
                DateOfBirth = Convert.ToDateTime(nm.BirthDate),
                Gender = nm.Gender,
                FacilityId = nm.OurFacId,
                MaritalSatus = nm.MaritalStatus,
                Diagnosis3 = "Test_MY"


            };
            var table = context.Set<ClientInfoTable>().Add(patient);
            // need to save chanches to get the new id that was just assigned By the database
            context.SaveChanges();
            //get the new id that was just assigned to the new clientinfo input aka ourIdma
            nm.OurPatientId = patient.ClientId;

            LogFile.Write($"Added To: ClientsInfoTable OurId: {patient.ClientId,-10} FirstName: {patient.OurFirstName,-15} LastName: {patient.LastName,-10} FacilityId: {patient.FacilityId,-10}");
        }

        LogFile.BreakLine();


        return patientsList;
    }
}
