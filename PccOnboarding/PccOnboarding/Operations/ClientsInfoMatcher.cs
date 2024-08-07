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
            if (patient.OutPatient)
            {
                LogFile.Write($"Outpatient - PccId: {patient.PatientId,-10} - FirstName: {patient.FirstName,-10} - LastName: {patient.LastName,-10} - pccId: {patient.SupCarePatientId,-10}\n");
            }
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.SupCareFirstName?.Trim().ToUpper() == patient.FirstName?.Trim().ToUpper() &&
                                    ourClient.SupCareLastName?.Trim().ToUpper() == patient.LastName?.Trim().ToUpper() &&
                                    ourClient.DateOfBirth.ToString() == patient.BirthDate);
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.SupCarePatientId = match.SupCareCleintId;
                //! we dont want to update the facility here because we need it later
            }
        }
        //~ FirstName LastName[0]
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.SupCareFirstName?.Trim().ToUpper() == patient.FirstName?.Trim().ToUpper() &&

                                    ourClient.SupCareLastName?.Split(' ', 2)[0].Trim().ToUpper() == patient.LastName.Split(' ', 2)[0].Trim().ToUpper() &&

                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.SupCarePatientId = match.SupCareCleintId;

            }
        }
        //~ FirstName[0] LastName
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.SupCareFirstName?.Split(' ', 2)[0].Trim().ToUpper() == patient.FirstName?.Split(' ', 2)[0].Trim().ToUpper() &&
                                    ourClient.SupCareLastName?.Trim()?.Trim().ToUpper() == patient.LastName.Trim().ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.SupCarePatientId = match.SupCareCleintId;
            }
        }
        //~ FirstName[0] LastName[0]
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    ourClient.SupCareFirstName?.Split(' ', 2)[0].Trim()?.ToUpper() == patient.FirstName?.Split(' ', 2)[0].Trim().ToUpper() &&

                                    ourClient.SupCareLastName?.Split(' ', 2)[0].Trim().ToUpper() == patient.LastName?.Split(' ', 2)[0].Trim().ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ?

                                    Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.SupCarePatientId = match.SupCareCleintId;
            }
        }
        //~ FirstName[1] LastName
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    (ourClient.SupCareFirstName?.Split(' ', 2).Length > 1 ? ourClient.SupCareFirstName?.Split(' ', 2)[1].Trim()?.ToUpper() : ourClient.SupCareFirstName) == (patient.FirstName?.Split(' ', 2).Length > 1 ? patient.FirstName?.Split(' ', 2)[1].Trim().ToUpper() : patient.FirstName) &&

                                    ourClient.SupCareLastName?.Trim().ToUpper() == patient.LastName?.ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ?

                                    Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.SupCarePatientId = match.SupCareCleintId;
            }
        }
        //~ FirstName[1] LastName[0]
        foreach (var patient in patientsList)
        {
            var match = ourClients.FirstOrDefault(ourClient =>
                                    (ourClient.SupCareFirstName?.Split(' ', 2).Length > 1 ? ourClient.SupCareFirstName?.Split(' ', 2)[1].Trim()?.ToUpper() : ourClient.SupCareFirstName) == (patient.FirstName?.Split(' ', 2).Length > 1 ? patient.FirstName?.Split(' ', 2)[1].Trim().ToUpper() : patient.FirstName) &&

                                    ourClient.SupCareLastName?.Split(' ', 2)[0].Trim().ToUpper() == patient.LastName?.Split(' ', 2)[0].Trim().ToUpper() &&
                                    (ourClient.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : ourClient.DateOfBirth.ToString()) == (patient.BirthDate == null ?

                                    Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(patient.BirthDate).ToString())
                                    );
            //* if its a matche we update the pcc list to show that this one is a match and set the clientId to have if for othersteps
            if (match != null)
            {
                patient.ClientInfoMatched = true;
                patient.SupCarePatientId = match.SupCareCleintId;
            }
        }

        var matchedOutpatients = patientsList.Where(x => x.ClientInfoMatched == true && x.OutPatient).ToList();
        LogFile.WriteWithBreak($"Found Matched Outpatients: {matchedOutpatients.Count,-10}");
        foreach (var patient in matchedOutpatients)
        {
            LogFile.Write($"outpatient match: {patient.FirstName,-10} {patient.LastName,-10} {patient.BirthDate,-10} {patient.OutPatient,-10}");
        }

        var matchedAmount = patientsList.Count(x => x.ClientInfoMatched == true);
        LogFile.WriteWithBreak($"Found Matched To ClientsInfoTable: {matchedAmount,-10}");
        await context.SaveChangesAsync();
        return patientsList;

    }
}
