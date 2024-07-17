using Microsoft.EntityFrameworkCore;
using PccOnboarding.models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Steps;

public class ClientsInfoMatcher : IOperation
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        LogFile.Write("Matching PCC patients to ClientsInfoTable...\n");


        var ourClients = context.Set<ClientInfoTable>().ToList();

        //check one 
        foreach (var patient in patientsList)
        {

            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.OurFirstName?.Split(' ', 2)[0] == patient.FirstName?.Trim().TrimEnd().TrimStart() &&
                                    (ourClient.LastName ?? "") == patient.LastName &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );

            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //Updates the facility id in the client info table
                match.FacilityId = patient.OurFacId;
            }


        }
        //second check
        foreach (var patient in patientsList)
        {

            var match = ourClients.FirstOrDefault(ourClient =>
                                    (ourClient.OurFirstName?.Split(' ', 2).Length > 1 ? ourClient.OurFirstName?.Split(' ', 2)[1].Trim() : ourClient.OurFirstName) == patient.FirstName?.Trim().TrimEnd().TrimStart() &&
                                    ourClient.OurFirstName?.Split(' ', 2)[0] == patient.FirstName?.Trim().TrimEnd().TrimStart() &&
                                    ourClient.LastName == patient.LastName &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );

            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.OurPatientId = match.ClientId;
                //Updates the facility id in the client info table
                match.FacilityId = patient.OurFacId;
            }


        }



        var matchedAmount = patientsList.Count(x => x.ClientInfoMatched == true);
        LogFile.Write($"Found Matched To ClientsInfoTable: {matchedAmount,-10}");
        context.SaveChanges();
        return patientsList;

    }
}
