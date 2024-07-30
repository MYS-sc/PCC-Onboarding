using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using PccDishargeSync;
using PccDishargeSync.Constants;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding.Operations;

public class ClientActiveAdder : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {


        LogFile.Write("Adding or Updating Client Active Table...\n");
        var table = await context.Set<ClientActiveTable>().ToListAsync();
        foreach (var patient in patientsList)
        {
            var clientInfo = await context.Set<ClientInfoTable>().FirstOrDefaultAsync(x => x.ClientId == patient.OurPatientId);
            var haveRecord = table.Where(x => x.ClientInfoId == patient.OurPatientId);
            if (haveRecord.Count() == 0)
            {
                goto Adder;
            }
            var matches = table.Where(x => x.ClientInfoId == patient.OurPatientId && x.DischargeDate == null);
            //this checks to see if he was discharge by use and not pcc
            if (clientInfo?.FacilityId == patient.OurFacId && matches.Count() == 0)
            {
                LogFile.Write($"------------did not update OurPatientId: {patient.OurPatientId}");
                continue;
            }

            //var matches = table.Where(x => x.ClientInfoId == patient.OurPatientId && x.DischargeDate == null);
            // if (matches.Count() == 0)
            // {
            //     goto Adder;
            // }

            if (matches.Count() > 0)
            {
                foreach (var m in matches)
                {
                    //var clientInfo = await context.Set<ClientInfoTable>().FirstOrDefaultAsync(x => x.ClientId == m.ClientInfoId);
                    if (clientInfo?.FacilityId == patient.OurFacId)
                    {
                        LogFile.Write($"Updating OurPatientId: {patient.OurPatientId}");
                        m.Bed = patient.BedDesc;
                        m.Room = patient.RoomDesc;
                        m.Floor = patient.FloorDesc;
                        m.OurFacilityId = patient.OurFacId;
                        continue;
                    }
                    m.OurFacilityId = clientInfo?.FacilityId;
                    m.DischargeDate = Convert.ToDateTime(patient.AdmissionDate).AddDays(-1);
                    m.TerminationType = TerminationTypesConsts.READMIT_DISCHARGE;
                }
            }
            var dischrageNull = table.Where(x => x.ClientInfoId == patient.OurPatientId && x.DischargeDate == null).ToList();
            if (dischrageNull.Count() > 0)
            {
                continue;
            }

        Adder:
            LogFile.Write($"Adding OurPatientId: {patient.OurPatientId}");
            ClientActiveTable clientActiveOne = new ClientActiveTable
            {
                ClientInfoId = patient.OurPatientId,
                ServiceType = 1,
                AdmissionDate = Convert.ToDateTime(patient.AdmissionDate),
                TerminationType = 0,
                Bed = patient.BedDesc,
                Floor = patient.FloorDesc,
                Room = patient.RoomDesc,
                OurFacilityId = patient.OurFacId,

            };
            ClientActiveTable clientActiveTwo = new ClientActiveTable
            {
                ClientInfoId = patient.OurPatientId,
                ServiceType = 2,
                AdmissionDate = Convert.ToDateTime(patient.AdmissionDate),
                TerminationType = 0,
                Bed = patient.BedDesc,
                Floor = patient.FloorDesc,
                Room = patient.RoomDesc,
                OurFacilityId = patient.OurFacId,

            };
            await context.AddAsync(clientActiveOne);
            await context.AddAsync(clientActiveTwo);


        }
        await context.SaveChangesAsync();
        LogFile.WriteWithBreak("Done adding or updating Client Active Table");
        //context.SaveChanges();
        return patientsList;
    }
}