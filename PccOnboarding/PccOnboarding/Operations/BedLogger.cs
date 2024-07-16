using Microsoft.EntityFrameworkCore;
using PccOnboarding.models.Our;
using PccOnboarding.Utils;

namespace PccOnboarding;

public class BedLogger : IOperation
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        //var options = new DbContextOptionsBuilder<TSC_Logs_Context>();
        context =  new TSC_Logs_Context();
        LogFile.Write("Logging Beds...\n");
        var table = context.Set<BedLogsTable>();
        foreach (var patient in patientsList)
        {

            var bedLog = new BedLogsTable
            {

                LastChangedDate = DateTime.Now.ToString(),
                ClientId = patient.OurPatientId,
                State = patient.State,
                PccId = patient.PatientId,
                BedDescription = patient.BedDesc,
                RoomDescription = patient.RoomDesc,
                FloorDescription = patient.FloorDesc,

            };
            table.Add(bedLog);
        }

        context?.SaveChanges();
        return patientsList;
    }

}
