using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Steps;

public class UpdateClientsInfoTableStep
{
    public IEnumerable<PatientsModel> Execute(IEnumerable<PatientsModel> patientsList, Type db, int? ourFacilityId)
    {
        LogFile.Write("Updating PatientsInfoTable...\n");
        //--Gets only the clients that we matched to our clients info table and now have an ClientId
        var ourPccClients = from patient in patientsList
                            where patient.ourId != null
                            select patient;

        //--Change the facility id to the one that he is in now
        using (var context = (DbContext)Activator.CreateInstance(db))
        {
            foreach (var client in ourPccClients)
            {
                //--Getting the pcc client from our client table
                var response = context.Set<ClientInfoTable>().First(c => c.ClientId == client.ourId);

                //--Saving the old facility id for logging reasons
                var oldFac = response.FacilityId;

                //--Updating the facility id in the clients info table
                response.FacilityId = ourFacilityId;

                //--Saving the changes
                context.SaveChanges();

                //--Quering the database after to check if it was changed
                var changed = context.Set<ClientInfoTable>().First(c => c.ClientId == client.ourId);

                //--Logging out all the changes to the console
                LogFile.Write($"Updated - ID: {client.ourId,-10} firstName: {client.FirstName,-10} lastname: {client.LastName,-10} FromFacility: {oldFac,-5} ToFacility: {changed.FacilityId,-5}");
            }
            LogFile.BreakLine();
        }
        return patientsList;
    }
}
