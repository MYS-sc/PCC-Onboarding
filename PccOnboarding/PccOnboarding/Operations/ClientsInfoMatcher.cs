using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class ClientsInfoMatcher : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        LogFile.Write("Matching PCC patients to ClientsInfoTable...\n");


        var ourClients = await context.Set<ClientInfoTable>().ToListAsync();//.Where(x => x.FacilityId == 3);
        if (ourClients.Count == 0)
        {
            return patientsList;
        }
        //* we need to check just the first name and last name and not the middel name so we need to split it to get rid of the middel name
        //* we do it twice because of the middel name sometimes is first and sometimes is second

        //~ FirstName LastName 
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.OurFirstName?.Trim().ToUpper() == patient.FirstName?.Trim().ToUpper() &&
                                    ourClient.LastName?.Trim().ToUpper() == patient.LastName?.Trim().ToUpper() &&
                                    ourClient.DateOfBirth.ToString() == patient.BirthDate);
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //*Updates the facility id in the client info table
                //match.FacilityId = patient.OurFacId;
            }
        }
        //~ FirstName LastName[0]
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.OurFirstName?.Trim().ToUpper() == patient.FirstName?.Trim().ToUpper() &&

                                    ourClient.LastName?.Split(' ', 2)[0].Trim().ToUpper() == patient.LastName.Split(' ', 2)[0].Trim().ToUpper() &&

                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //*Updates the facility id in the client info table
                //match.FacilityId = patient.OurFacId;
            }
        }
        //~ FirstName[0] LastName
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.OurFirstName?.Split(' ', 2)[0].Trim().ToUpper() == patient.FirstName?.Split(' ', 2)[0].Trim().ToUpper() &&
                                    ourClient.LastName?.Trim()?.Trim().ToUpper() == patient.LastName.Trim().ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //*Updates the facility id in the client info table
                //match.FacilityId = patient.OurFacId;
            }
        }
        //~ FirstName[0] LastName[0]
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.OurFirstName?.Split(' ', 2)[0].Trim()?.ToUpper() == patient.FirstName?.Split(' ', 2)[0].Trim().ToUpper() &&

                                    ourClient.LastName?.Split(' ', 2)[0].Trim().ToUpper() == patient.LastName?.Split(' ', 2)[0].Trim().ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ?

                                    Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //*Updates the facility id in the client info table
                //match.FacilityId = patient.OurFacId;
            }
        }
        //~ FirstName[1] LastName
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    (ourClient.OurFirstName?.Split(' ', 2).Length > 1 ? ourClient.OurFirstName?.Split(' ', 2)[1].Trim()?.ToUpper() : ourClient.OurFirstName) == (patient.FirstName?.Split(' ', 2).Length > 1 ? patient.FirstName?.Split(' ', 2)[1].Trim().ToUpper() : patient.FirstName) &&

                                    ourClient.LastName?.Trim().ToUpper() == patient.LastName?.ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ?

                                    Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //*Updates the facility id in the client info table
                //match.FacilityId = patient.OurFacId;
            }
        }
        //~ FirstName[1] LastName[0]
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    (ourClient.OurFirstName?.Split(' ', 2).Length > 1 ? ourClient.OurFirstName?.Split(' ', 2)[1].Trim()?.ToUpper() : ourClient.OurFirstName) == (patient.FirstName?.Split(' ', 2).Length > 1 ? patient.FirstName?.Split(' ', 2)[1].Trim().ToUpper() : patient.FirstName) &&

                                    ourClient.LastName?.Split(' ', 2)[0].Trim().ToUpper() == patient.LastName?.Split(' ', 2)[0].Trim().ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ?

                                    Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //*Updates the facility id in the client info table
                //match.FacilityId = patient.OurFacId;
            }
        }

        var matchedAmount = patientsList.Count(x => x.ClientInfoMatched == true);
        LogFile.WriteWithBreak($"Found Matched To ClientsInfoTable: {matchedAmount,-10}");
        await context.SaveChangesAsync();
        return patientsList;

    }
}
