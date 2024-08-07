using PccOnboarding.Models.PCC;
using PccOnboarding.Utils;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using PccOnboarding.Models.Our;

namespace PccOnboarding.Operations;

public class PccDischargedPatientsDataGetter : IDataGetter
{
    //AccessToken for the PCC API call
    private string _accessToken;
    private string pathToCertificate = @"C:\Users\MosheYehudaSznicer\Desktop\SuppCareApp.pfx";
    private string privateKeyPassphrase = "27Randolph";

    private string _orgId;
    private int _facId;
    private int? _ourFacId;
    private string _state;
    private int _page = 1;
    private List<OurPatientModel> _patientsList = new List<OurPatientModel>();


    public async Task<List<OurPatientModel>> Execute(string orgId, int facId, int? ourFacId, string state)
    {
        _orgId = orgId;
        _facId = facId;
        _ourFacId = ourFacId;
        _state = state;
        _accessToken = await BearerToken.Get();
        LogFile.Write($"Getting All Patients from PCC...\n");
        await _getPatient();
        LogFile.WriteWithBreak($"Got All Patients: {_patientsList.Count(),-10}");
        return _patientsList;
    }
    private async Task _getPatient()
    {
        X509Certificate2 clientCertificate = new X509Certificate2(pathToCertificate, privateKeyPassphrase);
        var handler = new HttpClientHandler();
        handler.ClientCertificates.Add(clientCertificate);
        var client = new HttpClient(handler);

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://connect2.pointclickcare.com/api/public/preview1/orgs/{_orgId}/patients?facId={_facId}&page={_page}&pageSize=200&patientStatus=Discharged"),
            Headers =
                {
                    {"contentType","application/json" },
                    {"authorization",$"Bearer {_accessToken}"}
                }

        };
        using (var reponse = await client.SendAsync(request))
        {
            reponse.EnsureSuccessStatusCode();

            var body = await reponse.Content.ReadAsStringAsync();

            //Gets the paging object
            PagingModel paging = JsonConvert.DeserializeObject<PagingModel>(body);
            //Deserializes the response to a list of patientsModel 
            var list = JsonConvert.DeserializeObject<PatientsListModel>(body);
            if (list == null || list.Data == null || list.Data.Count == 0)
            {
                throw new PccExeption("No patients found");
            }
            foreach (var item in list.Data)
            {
                item.SupCareFacId = _ourFacId;
                item.State = _state;
                //Adds each PatientModel to a list that i can call this function recursivly
                _patientsList?.Add(item);
            }
            //keep on running if there is more pages
            if (paging.Paging.HasMore == "true")
            {

                _page = ++_page;
                await _getPatient();


            }

        }
    }

}
