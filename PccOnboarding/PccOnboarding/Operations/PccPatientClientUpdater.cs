using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.Tables;
using PccOnboarding.Operations;
using PccOnboarding.Utils;
using System;

namespace PccOnboarding;

public class PccPatientClientUpdater : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientList, DbContext context)
    {
        LogFile.Write("Updating PccPatientsClientsTable...\n");
        var needUpdate = patientList.Where(x => x.ClientInfoMatched == false && x.PccMatched == true && x.IsSimilar == false).ToList();
        if (needUpdate.Count() == 0)
        {
            LogFile.WriteWithBreak("Nothing to Update PccPatientsClientsTable\n");
            return patientList;
        }
        foreach (var patient in needUpdate)
        {
            var clientInfo = await context.Set<PccPatientsClientTable>().FirstOrDefaultAsync(x => x.PccId == patient.PatientId);
            if (clientInfo != null)
            {

                clientInfo.SupCareClientId = patient.SupCarePatientId;
                clientInfo.TestUpdate = true;
                LogFile.Write($"Updated Patient - PccId: {patient.PatientId,-10} - FirstName: {patient.FirstName,-10} - LastName: {patient.LastName,-10} - OurPatientId: {patient.SupCarePatientId,-10}");
            }

        }
        await context.SaveChangesAsync();
        LogFile.WriteWithBreak("Done Updating PccPatientsClientsTable");
        return patientList;
    }
}
