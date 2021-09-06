using System;
using System.Net.Http;

namespace WiringDiagramConstructionMachine.Helper
{
    sealed class BsdHttpClient
    {
        private static HttpClient _httpClient = null;
        static internal HttpClient Instance()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(Configuration.Config.Ip);
            }

            return _httpClient;
        }
    }
}
