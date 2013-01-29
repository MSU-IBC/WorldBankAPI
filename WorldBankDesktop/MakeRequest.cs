using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorldBankDesktop
{
    class MakeRequest
    {
        string _address = "http://api.worldbank.org/";
        string _format = "&format=json";
        HttpClient _client = new HttpClient();
        JArray _JsonResponse = new JArray();

        public async Task<List<Indicators>> getAllIndicators()
        {
            var section = "indicators";
            await makeRequest(section, 1);
            await makeRequest(section, (int)_JsonResponse[0]["total"]);
            return JsonConvert.DeserializeObject<List<Indicators>>(_JsonResponse[1].ToString());
        }

        public async Task makeRequest(string section, int total)
        {
            HttpResponseMessage response = await _client.GetAsync(_address + "/" + section + "/?per_page=" + total + _format);
            response.EnsureSuccessStatusCode();
            _JsonResponse = JArray.Parse(await response.Content.ReadAsStringAsync());
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}
