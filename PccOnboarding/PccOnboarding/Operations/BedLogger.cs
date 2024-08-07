using Microsoft.EntityFrameworkCore;
using PccOnboarding.Context;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;


namespace PccOnboarding.Operations;

public class BedLogger : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {

        context = new TSC_Logs_Context();
        LogFile.Write("Logging Beds...\n");
        var table = context.Set<BedLogsTable>();
        foreach (var patient in patientsList)
        {

            var bedLog = new BedLogsTable
            {

                LastChangedDate = DateTime.Now.ToString(),
                SupCareClientId = patient.SupCarePatientId,
                State = patient.State,
                PccId = patient.PatientId,
                BedDescription = patient.BedDesc,
                RoomDescription = patient.RoomDesc,
                FloorDescription = patient.FloorDesc,

            };
            table.AddAsync(bedLog);
        }

        await context?.SaveChangesAsync();
        LogFile.WriteWithBreak("Done Logging Beds");
        return patientsList;
    }

}
