using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using PccDishargeSync;
using PccDishargeSync.Constants;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class ClientActiveAdder() : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        int updateCount = 0;
        int addCount = 0;
        int isDischargedCount = 0;
        int forcedDischargedCount = 0;

        LogFile.Write("Adding or Updating Client Active Table...\n");
        var table = await context.Set<ClientActiveTable>().ToListAsync();
        foreach (var patient in patientsList)
        {
            //! need to find out why i put that
            if (patient.SupCarePatientId == null)
            {
                continue;
            }
            //var haveRecord = table.Where(x => x.ClientInfoId == patient.OurPatientId);
            // this would mean that we dont have this client yet 
            if (patient.ClientInfoMatched == false)
            {
                goto Adder;
            }
            // this get the facility that its in now from the clientinfo table
            var clientInfo = await context.Set<ClientInfoTable>().FirstOrDefaultAsync(x => x.SupCareCleintId == patient.SupCarePatientId);

            var clientActiveMatches = table.Where(x => x.SupCareClientId == patient.SupCarePatientId && x.DischargeDate == null && x.AdmissionDate != null);
            //this checks to see if he was discharge by us and not pcc
            //! this can be an issue if he was discharged but was in the wrong facility the whole time
            if (clientInfo?.FacilityId == patient.SupCareFacId && !clientActiveMatches.Any())
            {
                isDischargedCount++;
                LogFile.Write($"------------did not update OurPatientId: {patient.SupCarePatientId}");
                continue;
            }

            if (clientActiveMatches.Count() > 0)
            {

                bool notLogged = true;
                foreach (var match in clientActiveMatches)
                {


                    if (clientInfo?.FacilityId == patient.SupCareFacId)
                    {

                        while (notLogged)
                        {
                            updateCount++;
                            LogFile.Write($"Updating OurPatientId: {patient.SupCarePatientId}");
                            notLogged = false;
                        }

                        match.Bed = patient.BedDesc;
                        match.Room = patient.RoomDesc;
                        match.Floor = patient.FloorDesc;
                        match.SupCareFacId = patient.SupCareFacId;
                        continue;
                    }

                    // if they were not in the same facility, we are going to force discharge them
                    while (notLogged)
                    {
                        LogFile.Write($"Force Discharge OurPatientId: {patient.SupCarePatientId}");
                        notLogged = false;
                    }

                    match.SupCareFacId = clientInfo?.FacilityId;
                    match.DischargeDate = Convert.ToDateTime(patient.AdmissionDate).AddDays(-1);
                    match.TerminationType = TerminationTypesConsts.READMIT_DISCHARGE;
                }
            }
            var dischrageNull = table.Where(x => x.SupCareClientId == patient.SupCarePatientId && x.DischargeDate == null).ToList();
            if (dischrageNull.Count() > 0)
            {
                continue;
            }

        Adder:
            LogFile.Write($"Adding OurPatientId: {patient.SupCarePatientId}");


            ClientActiveTable clientActiveOne = new ClientActiveTable
            {
                SupCareClientId = patient.SupCarePatientId,
                ServiceType = 1,
                AdmissionDate = DateTime.Today,
                TerminationType = 0,
                Bed = patient.BedDesc,
                Floor = patient.FloorDesc,
                Room = patient.RoomDesc,
                SupCareFacId = patient.SupCareFacId,

            };
            ClientActiveTable clientActiveTwo = new ClientActiveTable
            {
                SupCareClientId = patient.SupCarePatientId,
                ServiceType = 2,
                AdmissionDate = DateTime.Today,
                TerminationType = 0,
                Bed = patient.BedDesc,
                Floor = patient.FloorDesc,
                Room = patient.RoomDesc,
                SupCareFacId = patient.SupCareFacId,

            };
            addCount++;
            await context.AddAsync(clientActiveOne);
            await context.AddAsync(clientActiveTwo);


        }
        await context.SaveChangesAsync();

        LogFile.Write($"Added: {addCount} - Updated: {updateCount} - Was Discharged: {isDischargedCount}");
        LogFile.WriteWithBreak("Done adding or updating Client Active Table");
        //context.SaveChanges();
        return patientsList;
    }
}