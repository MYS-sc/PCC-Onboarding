// See https://aka.ms/new-console-template for more information


using PccOnboarding;
using PccOnboarding.Utils;
using PccOnboarding.Context;
using PccOnboarding.Models.PCC;
string orgId = "785e6a7d-206b-4421-b037-7a205c1d8e28";
int facId = 22;
string state = "PA";

var dbType = await GetContext.Get(state);
var ourFacId = await new OurFacilityId().Get(orgId, state, facId);
LogFile.Write($"StartTime: {DateTime.Now}");
//Gets the patients from the pcc api
var patients = await new GetPccDataStep(orgId, facId).Execute();
//Checks to see if we have those patients in our pccPatientsclients table and retuns the ones we don't have
var unmatched = new MatchPccPatientsClientStep().Execute(patients, dbType);
//Checks to see if we have these clients in the ClientsInfo table if we do we add our id and retun the ones we have and the ones we don't 
var matchedToOurClients = new MatchToOurClientsStep().Execute(unmatched, dbType);
//Adds the patients that we do have in the ClientsInfo table to the pccPatientsClients Table
var afterAddingMatched = new AddMatchedToPccClientsStep().Execute(matchedToOurClients, dbType, ourFacId);
//Updates the matched in the ClientsInfo table to the correct FacilityId
var afterUpdate = new UpdateClientsInfoTableStep().Execute(afterAddingMatched, dbType, ourFacId);
//Add the none Matched from the Clientsinfo table as new clients into the ClientsInfo table
var onlyNewClients = new AddNewClientsStep().Execute(afterUpdate, dbType, ourFacId);
//Adds the new clients to the pccPatientsClients table
new AddUnmatchedToPccClientsStep().Execute(onlyNewClients, dbType);
LogFile.WriteWithBrake($"EndTime: {DateTime.Now}");