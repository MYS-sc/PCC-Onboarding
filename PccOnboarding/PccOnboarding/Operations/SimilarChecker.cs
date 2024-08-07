﻿using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Models.Tables;
using PccOnboarding.Operations;
using PccOnboarding.Utils;

namespace PccOnboarding;

public class SimilarChecker : IOperation
{
    public async Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientsList, DbContext context)
    {
        LogFile.Write("Checking Similar ClientInfo Patients...\n");
        var table = await context.Set<ClientInfoTable>().ToListAsync();
        var notMatched = patientsList.Where(x => x.ClientInfoMatched == false).ToList();
        foreach (var patient in notMatched)
        {

            var match = table.FirstOrDefault(x => x.SupCareFirstName == patient.FirstName && x.DateOfBirth == Convert.ToDateTime(patient.BirthDate));
            if (match != null)
            {
                patient.IsSimilar = true;
                patient.SimilarPatientId = match.SupCareCleintId;
            }
        }
        foreach (var patient in notMatched)
        {

            var match = table.FirstOrDefault(x => x.SupCareLastName?.Trim().ToUpper() == patient.LastName.Trim().ToUpper() && x.DateOfBirth == Convert.ToDateTime(patient.BirthDate));
            if (match != null)
            {
                patient.IsSimilar = true;
                patient.SimilarPatientId = match.SupCareCleintId;
            }
        }
        foreach (var patient in notMatched)
        {
            var match = table.FirstOrDefault(x => x.SupCareFirstName?.Split(' ', 2)[0]?.Trim()?.ToUpper() == patient.FirstName?.Split(' ', 2)[0].Trim().ToUpper() && x.DateOfBirth == Convert.ToDateTime(patient.BirthDate));
            if (match != null)
            {
                patient.IsSimilar = true;
                patient.SimilarPatientId = match.SupCareCleintId;
            }
        }
        foreach (var patient in notMatched)
        {
            var match = table.FirstOrDefault(x => (x.SupCareFirstName?.Split(' ', 2).Length > 1 ? x.SupCareFirstName?.Split(' ', 2)[1].Trim().ToUpper() : x.SupCareFirstName) == (patient.FirstName?.Split(' ', 2).Length > 1 ? patient.FirstName.Split(' ', 2)[1] : patient.FirstName) && x.DateOfBirth == Convert.ToDateTime(patient.BirthDate));
            if (match != null)
            {
                patient.IsSimilar = true;
                patient.SimilarPatientId = match.SupCareCleintId;
            }
        }
        foreach (var patient in notMatched)
        {
            var match = table.FirstOrDefault(x => x.SupCareLastName?.Split(' ', 2)[0].Trim().ToUpper() == patient.LastName.Split(' ', 2)[0].Trim().ToUpper() && x.DateOfBirth == Convert.ToDateTime(patient.BirthDate));
            if (match != null)
            {
                patient.IsSimilar = true;
                patient.SimilarPatientId = match.SupCareCleintId;
            }
        }
        foreach (var patient in notMatched)
        {
            var match = table.FirstOrDefault(x => x.SupCareLastName?.Trim().ToUpper() == patient.LastName.Trim().ToUpper() && x.SupCareFirstName?.Trim().ToUpper() == patient.FirstName?.Trim().ToUpper());
            if (match != null)
            {
                patient.IsSimilar = true;
                patient.SimilarPatientId = match.SupCareCleintId;
            }
        }
        foreach (var patient in notMatched)
        {
            if (patient.IsSimilar)
            {
                Console.WriteLine(patient.FirstName + " " + patient.LastName + " " + patient.BirthDate + " " + patient.IsSimilar);
            }
        }

        var similarCount = patientsList.Where(x => x.IsSimilar == true).Count();
        if (similarCount == 0)
        {
            LogFile.WriteWithBreak("No Similar Patients");
            return patientsList;
        }
        LogFile.WriteWithBreak($"Similar Count: {similarCount}");
        return patientsList;
    }

}
