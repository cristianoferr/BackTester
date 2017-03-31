using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Backtester.backend.DataManager
{
    public class AcaoService
    {
        static string urlServico1 = "http://www.bitbolsa.com.br/charts/fetch.aspx?symbol=ACAO&interval=D&profileId=0&bars=1200&objects=true&adjustment=false&windowIndex=1&snapshot=&ts=1490981109557";

        //from=1/1/2013
        //to=28/02/2017
        static string urlServico = "https://br.advfn.com/common/javascript/tradingview/advfn/history?symbol=BOV%5EACAO&resolution=D&from=1356998400&to=1488240000";

        static string token = "o3MmBo73LsxmDMRIoAT4uMn8q55Y2u1TNXnT0jhseKUIs0sjvt0yDVAEISamNtOV--2jccrly7mnPm1YUJkbWxQA==";
        static string urlAPI = "http://www.bitbolsa.com.br/api";

        public static string LoadFromApi(string papel)
        {
            WebProxy myProxy = CreateProxy();
            var client = new RestClient(urlAPI);
            // client.Authenticator = new JwtAuthenticator(token);
            client.Proxy = myProxy;

            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("/grafico", Method.GET);
            // request.AddParameter("Access-Token", token); // adds to POST or URL querystring based on Method
            request.AddParameter("Accept", "text/plain");

            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");
            request.AddHeader("Access-Token", token);
            //request.AddUrlSegment("Access-Token", token);

            // add files to upload (works with compatible verbs)
            // request.AddFile(path);

            // execute the request
            IRestResponse response = client.Execute(request);


            var content = response.Content; // raw content as string

            /*
            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            RestResponse<Person> response2 = client.Execute<Person>(request);
            var name = response2.Data.Name;

            // easy async support
            client.ExecuteAsync(request, resp =>
            {
                Console.WriteLine(resp.Content);
            });

            // async with deserialization
            var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            {
                Console.WriteLine(response.Data.Name);
            });

            // abort the request on demand
            asyncHandle.Abort();*/

            return "";
        }

        public static string LoadFromApi2(string papel)
        {
            WebProxy myProxy = CreateProxy();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlAPI);

            client.DefaultRequestHeaders.Add("Access-Token", token);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("/grafico").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}", d);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return "";
        }

        public static string LoadFromWeb(string papel)
        {
            WebProxy myProxy = CreateProxy();

            string url = urlServico.Replace("ACAO", papel);
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Proxy = myProxy;

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();
            return responseFromServer;
        }

        private static WebProxy CreateProxy()
        {
            WebProxy myProxy = new WebProxy();
            Uri newUri = new Uri("http://127.0.0.1:3128");
            myProxy.Address = newUri;
            return myProxy;
        }

        internal static void RequestData(string papel, string fileName)
        {
            string saida = LoadFromWeb(papel);
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.WriteLine(saida);
            }
        }
    }
}
