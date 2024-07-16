using Microsoft.EntityFrameworkCore;
using PccOnboarding.models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;
using PccWebhook.Utils;

namespace PccOnboarding.Steps;

public class UpdateClientsInfoTableStep
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context, int? ourFacilityId)
    {
        LogFile.Write("Updating PatientsInfoTable...\n");
        //--Gets only the clients that we matched to our clients info table and now have an ClientId
        //var ourPccClients = from patient in patientsList
        //                    where patient.ourId != null
        //                    select patient;
        var matched = patientsList.Where(p => p.ClientInfoMatched == true);
        //--Change the facility id to the one that he is in now
        //using (var context = (DbContext)Activator.CreateInstance(db))
        {
            foreach (var client in matched)
            {
                //--Getting the pcc client from our client table
                var response = context.Set<ClientInfoTable>().First(c => c.ClientId == client.OurPatientId);

                //--Saving the old facility id for logging reasons
                var oldFac = response.FacilityId;

                //--Updating the facility id in the clients info table
                response.FacilityId = ourFacilityId;




                //--Quering the database after to check if it was changed
                //var changed = context.Set<ClientInfoTable>().First(c => c.ClientId == client.ourId);

                //--Logging out all the changes to the console
                LogFile.Write($"Updated - ID: {client.OurFacId,-10} firstName: {client.FirstName,-10} lastname: {client.LastName,-10} FromFacility: {oldFac,-5} ToFacility: {ourFacilityId,-5}");
            }
            //--Saving the changes
            context.SaveChanges();
            LogFile.BreakLine();
        }
        return patientsList;
    }
}
