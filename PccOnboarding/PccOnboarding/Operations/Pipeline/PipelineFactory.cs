using Microsoft.EntityFrameworkCore;
using PccOnboarding.Constants;
using PccOnboarding.Models.Our;
using PccOnboarding.Operations;

namespace PccOnboarding;

public class PipelineFactory : IPipelineFactory
{

    public Pipeline<OurPatientModel> Create(string runType)
    {
        return runType switch
        {
            RunTypes.ONBOARDING => new Pipeline<OurPatientModel>()
                                        .AddDataGetter(new PccCurrentPatientsDataGetter())
                                        .Add(new PccPatientsClientMatcher())
                                        .Add(new ClientsInfoMatcher())
                                        .Add(new ClientInfoMatchedPccPatientsClientAdder())
                                        .Add(new NewClientsAdder())
                                        .Add(new AddUnmatchedToPccClientsStep())
                                        .Add(new ClientActiveAdder())
                                        .Add(new BedLogger()),

            RunTypes.DISCHARGE_SYNC => new Pipeline<OurPatientModel>()
                                            .AddDataGetter(new PccDischargedPatientsDataGetter())
                                            .Add(new ClientsInfoMatcher())
                                            .Add(new ClientActiveDischarger())
        };
    }
}
