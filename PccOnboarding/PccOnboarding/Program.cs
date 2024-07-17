// See https://aka.ms/new-console-template for more information


using PccOnboarding;
using PccOnboarding.Utils;
using PccOnboarding.Context;
using PccOnboarding.Models.PCC;
using PccOnboarding.Steps;
using PccWebhook.Utils;


using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PccOnboarding.models.Our;

var serviceCollection = new ServiceCollection();

serviceCollection.AddContexts();
serviceCollection.AddScoped<IContextFactory, ContextFactory>();

var serviceProvider = serviceCollection.BuildServiceProvider();



string orgId = "785e6a7d-206b-4421-b037-7a205c1d8e28";
int facId = 22;
string state = "PA";

//var dbType = await GetContext.Get(state);
DbContext dbContext = serviceProvider.GetRequiredService<IContextFactory>().GetContext(state);


var ourFacId = await new OurFacilityId().Get(orgId, facId, dbContext);
LogFile.Write($"StartTime: {DateTime.Now}");
//Gets the patients from the pcc api
var patients = await new PccDataGetter().Execute(orgId, facId, ourFacId, state);


var pipeline = new Pipeline<OurPatientModel>();

pipeline.Add(new PccPatientsClientMatcher())
        .Add(new ClientsInfoMatcher())
        .Add(new ClientInfoMatchedPccPatientsClientAdder())
        .Add(new NewClientsAdder())
        .Add(new AddUnmatchedToPccClientsStep())
        .Add(new ClientActiveAdder())
        .Add(new BedLogger());

pipeline.Execute(patients, dbContext);

dbContext.SaveChanges();

LogFile.WriteWithBreak($"EndTime: {DateTime.Now}");


//Test stuff

//var adtList = await new GettingAdtRecordsStep().Execute(matchedToOurClients, orgId);
//var a = await new CoveragesStepTest().Execute(matchedToOurClients, orgId);
//var a = await new WebHookSubscriber().post();
//var g = await new WebHookChecker().get();
//new InsertAtdRecordsStep().Execute(adtList, dbType);