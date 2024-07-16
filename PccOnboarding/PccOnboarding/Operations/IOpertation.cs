using Microsoft.EntityFrameworkCore;
using PccOnboarding.models.Our;

namespace PccOnboarding;

public interface IOperation
{
    List<OurPatientModel> Execute(List<OurPatientModel> patientList, DbContext context);
}
