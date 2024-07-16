using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using PccOnboarding.models.Our;
using PccOnboarding.Models.PCC;

namespace PccOnboarding;

public class ClientActiveAdder : IOperation
{
    public List<OurPatientModel> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        var table = context.Set<ClientActiveTable>();
        Console.WriteLine(patientsList.Count());
        foreach (var patient in patientsList)
        {

            var matches = table.Where(x => x.ClientInfoId == patient.OurPatientId && x.DischargeDate == null).ToList();
            if (matches.Count() == 0)
            {
                goto Addre;
            }

            if (matches.Count() > 0)
            {
                foreach (var m in matches)
                {
                    if (m.AdmissionDate == Convert.ToDateTime(patient.AdmissionDate))
                    {
                        continue;
                    }
                    m.DischargeDate = Convert.ToDateTime(patient.AdmissionDate).AddDays(-1);
                    m.TerminationType = 7;
                }
            }
            var dischrageNull = table.Where(x => x.ClientInfoId == patient.OurPatientId && x.DischargeDate == null).ToList();
            if (dischrageNull.Count() > 0)
            {
                return patientsList;
            }

        Addre:
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
        //context.SaveChanges();
        return patientsList;
    }
}