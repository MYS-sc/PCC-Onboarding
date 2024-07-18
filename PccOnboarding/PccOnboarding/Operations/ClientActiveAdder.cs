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
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        LogFile.Write("Adding or Updating Client Active Table...");
        var table = context.Set<ClientActiveTable>();
        foreach (var patient in patientsList)
        {

            var matches = table.Where(x => x.ClientInfoId == patient.OurPatientId && x.DischargeDate == null);
            if (matches.Count() == 0)
            {
                goto Adder;
            }

            if (matches.Count() > 0)
            {
                foreach (var m in matches)
                {
                    if (m.AdmissionDate == Convert.ToDateTime(patient.AdmissionDate))
                    {
                        LogFile.Write($"Updating OurPatientId: {patient.OurPatientId}\n");
                        m.Bed = patient.BedDesc;
                        m.Room = patient.RoomDesc;
                        m.Floor = patient.FloorDesc;
                        m.OurFacilityId = patient.OurFacId;
                        continue;
                    }
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
            LogFile.Write($"Adding OurPatientId: {patient.OurPatientId}\n");
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
            context.Add(clientActiveOne);
            context.Add(clientActiveTwo);


        }
        context.SaveChanges();
        LogFile.WriteWithBreak("Done adding or updating Client Active Table");
        //context.SaveChanges();
        return patientsList;
    }
}