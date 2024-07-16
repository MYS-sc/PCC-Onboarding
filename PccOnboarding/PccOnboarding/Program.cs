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
var patients = await new PccDataGetter().Execute(orgId, facId, ourFacId);


var pipeline = new Pipeline<OurPatientModel>();
pipeline.Add(new PccPatientsClientMatcher())
    .Add(new ClientsInfoMatcher())
    .Add(new ClientInfoMatchedPccPatientsClientAdder())
    .Add(new AddUnmatchedToPccClientsStep())
    .Add(new ClientActiveAdder());

// var matchedToPcc = new PccPatientsClientMatcher().Execute(patients, dbContext);

// var matchedToClientsInfo = new ClientsInfoMatcher().Execute(matchedToPcc, dbContext);

// var afterAddingMatched = new ClientInfoMatchedPccPatientsClientAdder().Execute(matchedToClientsInfo, dbContext);

// var afterAddingNewClients = new NewClientsAdder().Execute(afterAddingMatched, dbContext);

// var afterAddingToPccClientsPatients = new AddUnmatchedToPccClientsStep().Execute(afterAddingNewClients, dbContext);

// new ClientActiveAdder().Execute(afterAddingToPccClientsPatients, dbContext);




pipeline.Execute(patients, dbContext);




//Updates the matched in the ClientsInfo table to the correct FacilityId
//var afterUpdate = new UpdateClientsInfoTableStep().Execute(afterAddingMatched, dbContext, ourFacId);
//new ClientActiveAdder().Execute(matchedToOurClients, ourFacId, dbContext);
//Add the none Matched from the Clientsinfo table as new clients into the ClientsInfo table

//Adds the new clients to the pccPatientsClients table


//new ClientActiveAdder().Execute(afterAddingToPccClientsPatients, ourFacId, dbContext);

dbContext.SaveChanges();
LogFile.WriteWithBreak($"EndTime: {DateTime.Now}");


//Test stuff

//var adtList = await new GettingAdtRecordsStep().Execute(matchedToOurClients, orgId);
//var a = await new CoveragesStepTest().Execute(matchedToOurClients, orgId);
//var a = await new WebHookSubscriber().post();
//var g = await new WebHookChecker().get();
//new InsertAtdRecordsStep().Execute(adtList, dbType);