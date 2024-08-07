using Microsoft.EntityFrameworkCore;
using PccOnboarding.Constants;
using PccOnboarding.Models.Our;
using PccOnboarding.Operations;

namespace PccOnboarding;

public class PipelineFactory : IPipelineFactory
{

    public Pipeline Create(string runType)
    {
        return runType switch
        {
            RunTypes.ONBOARDING => new Pipeline()
                                        .AddDataGetter(new PccCurrentPatientsDataGetter())
                                        .Add(new PccPatientsClientMatcher())
                                        .Add(new ClientsInfoMatcher())
                                        .Add(new ClientInfoMatchedPccPatientsClientAdder())
                                        .Add(new SimilarChecker())
                                        .Add(new SimilarLogger())
                                        .Add(new NewClientsAdder())
                                        .Add(new PccPatientClientUpdater())
                                        .Add(new AddUnmatchedToPccClientsStep())
                                        .Add(new ClientActiveAdder())
                                        .Add(new ClientInfoUpdater())
                                        .Add(new TestLogger())
                                        .Add(new BedLogger()),

            RunTypes.DISCHARGE_SYNC => new Pipeline()
                                        .AddDataGetter(new PccDischargedPatientsDataGetter())
                                        .Add(new PccPatientsClientMatcher())
                                        .Add(new ClientActiveDischarger()),
            _ => throw new NotImplementedException()
        };
    }
}
