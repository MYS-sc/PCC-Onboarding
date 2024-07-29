using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;

namespace PccOnboarding.Operations;

public interface IOperation
{
    Task<List<OurPatientModel>> Execute(List<OurPatientModel> patientList, DbContext context);
}
