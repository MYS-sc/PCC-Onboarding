using System.Net;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using PccOnboarding.Utils;

namespace PccOnboarding;

public class WebHookChecker
{
    private string _accessToken;

    private string pathToCertificate = @"C:\Users\MosheYehudaSznicer\Desktop\SuppCareApp.pfx";
    private string privateKeyPassphrase = "27Randolph";
    private static string name = "supportivecaredev";
    string url = $"https://connect2.pointclickcare.com/api/public/preview1/webhook-subscriptions?applicationName={name}";

    public async Task<HttpStatusCode> get()
    {
        _accessToken = await BearerToken.Get();
        X509Certificate2 clientCertificate = new X509Certificate2(pathToCertificate, privateKeyPassphrase);
        var handler = new HttpClientHandler();
        handler.ClientCertificates.Add(clientCertificate);
        var client = new HttpClient(handler);
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Headers ={
                {"contentType","application/json"},
                {"authorization",$"Bearer {_accessToken}"}
            }
        };
        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(JValue.Parse(body).ToString(Formatting.Indented));
        Console.WriteLine(body);
        return response.StatusCode;
    }
}
