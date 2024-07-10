using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Steps;

public class AddNewClientsStep
{
    public IEnumerable<PatientsModel> Execute(IEnumerable<PatientsModel> patientsList, DbContext context, int? ourFacId)
    {
        LogFile.Write("Adding new Clients...\n");
        var notMatched = from patient in patientsList
                         where patient.ourId == null
                         select patient;
        //using (var context = (DbContext)Activator.CreateInstance(db))
        {
            foreach (var nm in notMatched)
            {


                var patient = new ClientInfoTable
                {
                    OurFirstName = nm.FirstName,
                    LastName = nm.LastName,
                    DateOfBirth = Convert.ToDateTime(nm.BirthDate),
                    Gender = nm.Gender,
                    FacilityId = ourFacId,
                    MaritalSatus = nm.MaritalStatus,
                    Diagnosis3 = "Test_MY"


                };
                var table = context.Set<ClientInfoTable>().Add(patient);
                //context.SaveChanges();
                //get the new id that was just assigned to the new clientinfo input aka ourIdma
                patientsList = patientsList.Select(p =>
                                                    {
                                                        if (p.PatientId == nm.PatientId)
                                                        {
                                                            p.ourId = patient.ClientId;
                                                        }
                                                        return p;
                                                    }).ToList();
                LogFile.Write($"Added To: ClientsInfoTable OurId: {patient.ClientId,-10} FirstName: {patient.OurFirstName,-15} LastName: {patient.LastName,-10} FacilityId: {patient.FacilityId,-10}");
            }
            LogFile.BreakLine();
        }

        return patientsList;
    }
}
