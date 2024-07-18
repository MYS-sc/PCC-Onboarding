using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;

namespace PccOnboarding.Operations;

public interface IPipelineFactory
{
    Pipeline<OurPatientModel> Create(string runType);
}
