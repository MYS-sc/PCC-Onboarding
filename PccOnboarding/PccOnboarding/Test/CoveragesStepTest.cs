using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using PccOnboarding.Models.PCC;
using PccOnboarding.Models.Tables;
using PccOnboarding.Utils;

namespace PccOnboarding;

public class CoveragesStepTest
{
    private string _accessToken;

    private string pathToCertificate = @"C:\Users\MosheYehudaSznicer\Desktop\SuppCareApp.pfx";
    private string privateKeyPassphrase = "27Randolph";
    private List<PccAdtTable> _adtList = new List<PccAdtTable>();
    public async Task<List<PccAdtTable>?> Execute(IEnumerable<PccPatientsModel> patientsList, string orgId)
    {
        _accessToken = await BearerToken.Get();
        foreach (var patient in patientsList)
        {
            await _getAdtRecord(orgId, patient.PatientId);
        }
        return _adtList;
    }
    private async Task<string> _getAdtRecord(string orgId, int patientId)
    {
        X509Certificate2 clientCertificate = new X509Certificate2(pathToCertificate, privateKeyPassphrase);
        var handler = new HttpClientHandler();
        handler.ClientCertificates.Add(clientCertificate);
        var client = new HttpClient(handler);
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://connect2.pointclickcare.com/api/public/preview1/orgs/{orgId}/coverages?patientId={patientId}"),
            Headers ={
                {"contentType","application/json"},
                {"authorization",$"Bearer {_accessToken}"}
            }
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(JValue.Parse(body).ToString(Formatting.Indented));
            var adtList = JsonConvert.DeserializeObject<CoveragesModel>(body);
            foreach (var adt in adtList.Payers)
            {
                Console.WriteLine(adt.PayerName);
            }
            return "done";
        }
    }
}
