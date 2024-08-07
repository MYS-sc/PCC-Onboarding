

using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;

namespace PccOnboarding.Operations;

public class TestLogger : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientList, DbContext context)
    {

        var matched = patientList.Where(p => p.ClientInfoMatched == true && p.IsSimilar == false).ToList();
        // Console.WriteLine($"Matched: {matched.Count}");
        // foreach (var patient in matched){
        //     Console.WriteLine($"OurPatientId: {patient.OurPatientId}, PccPatientId: {patient.PatientId}");
        // }

        int matchedCount = matched.Count;
        int count = 1;

        TestFileLogger.Write("[");
        foreach (var patient in matched)
        {
            if (count == matchedCount)
            {
                TestFileLogger.Write($"{{ \"ourClientId\" : {patient.SupCarePatientId}, \n \"pccClientId\" : {patient.PatientId} \n}}");
                count++;
                continue;
            }
            TestFileLogger.Write($"{{ \"ourClientId\" : {patient.SupCarePatientId}, \n \"pccClientId\" : {patient.PatientId} \n}},");
            count++;
        }

        TestFileLogger.Write("]");
        return patientList;
    }
}
