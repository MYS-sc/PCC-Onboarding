using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;

namespace PccOnboarding;

public class MatchToOurClientsStep
{
    public IEnumerable<PatientsModel> Execute(IEnumerable<PatientsModel> patientsList, Type db)
    {
        LogFile.Write("Matching PCC patients to ClientsInfoTable...\n");
        var matched = 0;
        using (var context = (DbContext)Activator.CreateInstance(db))
        {
            var ourClients = context.Set<ClientInfoTable>().ToList();

            var patientsCheckOne = from Pcc in patientsList
                                   join OC in ourClients
                                   on new { F = Pcc.FirstName?.Trim().TrimEnd().TrimStart(), L = Pcc.LastName, D = Pcc.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(Pcc.BirthDate).ToString() }
                                   equals new { F = OC.OurFirstName?.Split(' ', 2)[0], L = OC.LastName ?? "", D = OC.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : OC.DateOfBirth.ToString() }
                                   into groupJoin
                                   from subgroup in groupJoin.DefaultIfEmpty()
                                   select new
                                   {
                                       Pcc.PatientId,
                                       Pcc.BirthDate,
                                       Pcc.FirstName,
                                       Pcc.LastName,
                                       Pcc.MiddleName,
                                       Pcc.OrgUuid,
                                       Pcc.FacId,
                                       ourId = Pcc.ourId ?? subgroup?.ClientId,
                                   };
            var patientsCheckThree = from Pcc in patientsCheckOne
                                     join OC in ourClients
                                     on new { F = Pcc.FirstName?.Trim().TrimEnd().TrimStart(), L = Pcc.LastName, D = Pcc.BirthDate == null ? Convert.ToDateTime("1/1/1990").ToString() : Convert.ToDateTime(Pcc.BirthDate).ToString() }
                                     equals new { F = OC.OurFirstName?.Split(' ', 2).Length > 1 ? OC.OurFirstName?.Split(' ', 2)[1].Trim() : OC.OurFirstName, L = OC.LastName ?? "", D = OC.DateOfBirth == null ? Convert.ToDateTime("1/1/1990").ToString() : OC.DateOfBirth.ToString() }
                                     into groupJoin
                                     from subgroup in groupJoin.DefaultIfEmpty()
                                     select new PatientsModel
                                     {
                                         PatientId = Pcc.PatientId,
                                         BirthDate = Pcc.BirthDate,
                                         FirstName = Pcc.FirstName,
                                         LastName = Pcc.LastName,
                                         MiddleName = Pcc.MiddleName,
                                         OrgUuid = Pcc.OrgUuid,
                                         FacId = Pcc.FacId,
                                         ourId = Pcc.ourId ?? subgroup?.ClientId,
                                     };
            foreach (var p in patientsCheckThree)
            {
                if (p.ourId != null)
                {
                    matched++;
                }
            }
            LogFile.Write($"Found Matched To ClientsInfoTable: {matched}");
            LogFile.WriteWithBrake($"Found Unmatched To ClientsInfoTable: {patientsCheckThree.Count() - matched}");

            return patientsCheckThree;
        }
    }
}
