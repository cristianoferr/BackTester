using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using UsoComum;

namespace Fundspider.Backend
{
    public class BuscaPapeis
    {
        public static string URL_LISTA = "http://br.advfn.com/bolsa-de-valores/bovespa/";

        /**
         * Retorna uma lista de papeis válidos
         * Só retorna papeis que terminam em '3'
         */
        public  IList<string> PegaListaUrls()
        {
            IList<string> ret = new List<string>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string url = URL_LISTA + c;
                string saidaLista = LoadFromWeb(url);
                AnalisaSaida(saidaLista, ret);
            }
            return ret;
        }

        public  void AnalisaSaida(string saidaLista, IList<string> ret)
        {
            throw new NotImplementedException();
        }

        public  string LoadFromWeb(string url)
        {
            WebProxy myProxy = CreateProxy();

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
            string proxy = Environment.GetEnvironmentVariable("HTTP_PROXY");
            if (proxy == null || proxy == "")
                return null;

            WebProxy myProxy = new WebProxy();
            Uri newUri = new Uri(proxy.Contains("http") ? proxy : "http://" + proxy);
            myProxy.Address = newUri;
            return myProxy;
        }
    }
}
