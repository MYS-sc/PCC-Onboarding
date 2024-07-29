
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace PccOnboarding.Utils
{
    public static class BearerToken
    {

        private const string pathToCertificate = "C:\\Users\\MosheYehudaSznicer\\Desktop\\SuppCareApp.pfx";

        private const string privateKeyPassphrase = "27Randolph";
        //! only for sandbox use 
        //private const string auth_password = "Jx3U4LfMh3nYb1YO";
        //private const string auth_cl_id = "wrLmehEJwBJrL7XYn8CboLLsYsKnbW44";
        //! Only for production use
        private const string auth_password = "uA8wTxiroRNmYXG5";
        private const string auth_cl_id = "LxGhD5OHvAS2bSjCOh9lN5AoFBoVJYf7";
        public static async Task<string> Get()
        {
            var url = "https://connect2.pointclickcare.com/auth/token";
            var plainTextBytes = Encoding.UTF8.GetBytes(auth_cl_id + ":" + auth_password);
            var certificate = new X509Certificate2(pathToCertificate, privateKeyPassphrase);
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(certificate);
            var client = new HttpClient(handler);



            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers = {
                    {"authorization",$"Basic {Convert.ToBase64String(plainTextBytes)}"},
                    {"contect-type","application/x-www-form-urlencoded"}
                },
                Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded")

            };

            using (var reponse = await client.SendAsync(request))
            {
                reponse.EnsureSuccessStatusCode();
                var body = await reponse.Content.ReadAsStringAsync();
                body = body.Replace("{\"access_token\":\"", "");
                body = body.Replace("\",\"expires_in\":\"7199\"}", "");
                Console.WriteLine(body);
                return body;
            }
        }
    }
}
