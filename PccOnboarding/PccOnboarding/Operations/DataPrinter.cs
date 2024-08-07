using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Operations;

namespace PccOnboarding;

public class DataPrinter : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientList, DbContext context)
    {

        foreach (var patient in patientList)
        {
            if (patient.PatientId == 541658)
            {
                Console.WriteLine(patient.PatientId);
            }

        }
        return patientList;
    }
}
