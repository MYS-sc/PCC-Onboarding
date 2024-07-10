using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using PccOnboarding.Utils;

namespace PccOnboarding;
public class Webhoohsubscription
{
    public string applicationName { get; set; }
    public bool enableRoomReservationCancellation { get; set; }
    public string endUrl { get; set; }
    public string[] eventGroupList { get; set; }
    public bool includeDischarge { get; set; }
    public bool includeOutpatient { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    //public string VendorExternalId { get; set; }
}

public class WebHookSubscriber
{
    private string _accessToken;

    private string pathToCertificate = @"C:\Users\MosheYehudaSznicer\Desktop\SuppCareApp.pfx";
    private string privateKeyPassphrase = "27Randolph";
    private static string name = "supportivecaredev";
    string url = $"https://connect2.pointclickcare.com/api/public/preview1/webhook-subscriptions";

    public async Task<HttpStatusCode> post()
    {
        _accessToken = await BearerToken.Get();
        X509Certificate2 clientCertificate = new X509Certificate2(pathToCertificate, privateKeyPassphrase);
        var handler = new HttpClientHandler();
        handler.ClientCertificates.Add(clientCertificate);
        var client = new HttpClient(handler);

        Webhoohsubscription scema = new Webhoohsubscription()
        {
            applicationName = name,
            enableRoomReservationCancellation = false,
            endUrl = "https://3flt76lf-8080.use.devtunnels.ms/",
            eventGroupList = [
                "ADT02",
                "ADT01",
                "ADT06"

            ],
            includeDischarge = false,
            includeOutpatient = false,
            username = "testApiMy",
            password = "testapi123"

        };

        var json = JsonConvert.SerializeObject(scema);
        Console.WriteLine(json);
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(url),
            Headers ={
                {"contentType","application/json"},
                {"authorization",$"Bearer {_accessToken}"}
            },
            Content = new StringContent(json, Encoding.UTF8, "application/json")

        };
        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(JValue.Parse(body).ToString(Formatting.Indented));
        Console.WriteLine(body);

        return response.StatusCode;
    }


}
