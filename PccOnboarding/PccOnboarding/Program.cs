
using PccOnboarding;
using PccOnboarding.Utils;
using PccOnboarding.Operations;
using PccWebhook.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Our;
using PccOnboarding.Constants;
using PccOnboarding.Context;



ServiceCollection serviceCollection = new ServiceCollection();

serviceCollection.AddContexts();
serviceCollection.AddScoped<IContextFactory, ContextFactory>();
serviceCollection.AddScoped<IPipelineFactory, PipelineFactory>();

ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();



string orgId = "785e6a7d-206b-4421-b037-7a205c1d8e28";
int facId = 22;
string state = "PA";
//TODO should put this all in its own class
//* sets all the variables that we will need from the user 
string runType;
int? ourFacId;
DbContext dbContext;
List<OurPatientModel> patients = new List<OurPatientModel>();

//*Only can enter 1 or 2
while (true)
{
        Console.Write("Please Enter one of the following - Just enter the number: (1 - Onboarding, 2 - Discharge) > ");
        var input = Console.ReadLine();
        if (!(input == "1" || input == "2"))
        {
                continue;
        }

        runType = input;
        break;
}

//* only will pass if we have this OrgUid, FacId and State Combo in our database
while (true)
{
        Console.Write("Please Enter orgUId > ");
        orgId = Console.ReadLine();
        Console.Write("Please Enter FacId > ");
        facId = int.Parse(Console.ReadLine());
        Console.Write("Please Enter State > ");
        state = Console.ReadLine().ToUpper();
        //!for testing only take out for production
        state = "TEST";
        dbContext = serviceProvider.GetRequiredService<IContextFactory>().GetContext(state);

        ourFacId = await new OurFacilityId().Get(orgId, facId, dbContext);

        if (ourFacId != null)
        {
                break;
        }

        Console.WriteLine("Facility not found Please try again");
}

LogFile.Write($"StartTime: {DateTime.Now}");
Console.WriteLine(dbContext.GetType().Name);
//* Get the right pipeline that we need Onboarding or Discharge
var pipeline = serviceProvider.GetRequiredService<IPipelineFactory>().Create(runType);

await pipeline.Execute(orgId, facId, ourFacId, state, dbContext);

dbContext.SaveChanges();

LogFile.WriteWithBreak($"EndTime: {DateTime.Now}");


//Test stuff

//var adtList = await new GettingAdtRecordsStep().Execute(matchedToOurClients, orgId);
//var a = await new CoveragesStepTest().Execute(matchedToOurClients, orgId);
//var a = await new WebHookSubscriber().post();
//var g = await new WebHookChecker().get();
//new InsertAtdRecordsStep().Execute(adtList, dbType);