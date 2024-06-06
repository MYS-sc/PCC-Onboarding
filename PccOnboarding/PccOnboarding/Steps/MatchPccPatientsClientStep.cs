using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;

namespace PccOnboarding;

public class MatchPccPatientsClientStep
{
    public IEnumerable<PatientsModel> Execute(List<PatientsModel> patientsList, Type db)
    {
        LogFile.Write("Matching to PccPatientsClients Table...\n");
        using (var context = (DbContext)Activator.CreateInstance(db))
        {
            var data = context.Set<PccPatientsClientTable>().ToList();
            var unmatchedData = from p in patientsList
                                join d in data
                                    on new { F = p.FirstName, L = p.LastName, O = p.OrgUuid, FI = p.FacId, D = Convert.ToDateTime(p.BirthDate).ToString(), PI = p.PatientId.ToString() }
                                    equals new { F = d.FirstName, L = d.LastName, O = d.OrgUid, FI = d.FacilityId.ToString(), D = Convert.ToDateTime(d.PccDob.ToString()).ToString(), PI = d.PccId.ToString() }
                                    into gj
                                from subgroup in gj.DefaultIfEmpty()
                                where subgroup == null
                                select new PatientsModel
                                {
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    MiddleName = p.MiddleName,
                                    OrgUuid = p.OrgUuid,
                                    FacId = p.FacId,
                                    BirthDate = p.BirthDate,
                                    PatientId = p.PatientId,
                                    Gender = p.Gender == "FEMALE" ? "Female" : "Male",
                                    MaritalStatus = p.MaritalStatus,
                                    ourId = null

                                };
            LogFile.WriteWithBrake($"Found unmatched to PccPatientsClient Table: {unmatchedData.Count(),-10}");
            return unmatchedData;
        }
    }
}
