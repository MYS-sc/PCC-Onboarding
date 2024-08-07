﻿using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using PccDishargeSync;
using PccDishargeSync.Constants;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;

namespace PccOnboarding.Operations;

public class ClientActiveDischarger : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        var table = context.Set<ClientActiveTable>();

        foreach (var patient in patientsList)
        {
            var match = table.Where(table => table.SupCareClientId == patient.SupCarePatientId && table.AdmissionDate == Convert.ToDateTime(patient.AdmissionDate) && table.DischargeDate == null);
            foreach (var m in match)
            {
                m.DischargeDate = Convert.ToDateTime(patient.DischargeDate);
                if (patient.Deceased)
                {
                    m.TerminationType = TerminationTypesConsts.DECEASED;
                    continue;
                }
                m.TerminationType = TerminationTypesConsts.DISCHARGED;

            }

        }
        await context.SaveChangesAsync();
        //context.SaveChanges();
        return patientsList;
    }
}