using Microsoft.EntityFrameworkCore;
using PccOnboarding.Context;
using PccOnboarding.Models.Our;
using PccOnboarding.Operations;
using PccOnboarding.Utils;

namespace PccOnboarding;

public class SimilarLogger : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        var similarCount = patientsList.Where(x => x.IsSimilar).Count();
        if (similarCount == 0)
        {
            return patientsList;
        }

        LogFile.Write("Logging Similar Patients...\n");
        using var logsContext = new TSC_Logs_Context();
        var similarList = patientsList.Where(x => x.IsSimilar).ToList();
        foreach (var similar in similarList)
        {
            var log = new OnboardingSimilarLogsTable
            {
                OrgUuid = similar.OrgUuid,
                PccFacId = similar.FacId,
                PccId = similar.PatientId,
                FirstName = similar.FirstName,
                LastName = similar.LastName,
                DateOfBirth = similar.BirthDate,
                //OurId = similar.OurPatientId,
                OurFacId = similar.OurFacId,
                State = similar.State,
                SimilarPatientId = similar.SimilarPatientId

            };
            await logsContext.OnboardingSimilarLogsTable.AddAsync(log);
            LogFile.Write($"Logged Similar Patient - PccId: {similar.PatientId} - FirstName: {similar.FirstName} - LastName: {similar.LastName} -- SimilarPatientId: {similar.SimilarPatientId}");
        }
        await logsContext.SaveChangesAsync();
        LogFile.WriteWithBreak("Done Logging Similar Patients");
        return patientsList;
    }
}
