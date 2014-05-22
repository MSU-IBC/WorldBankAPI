using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorldBank.Models;

namespace WorldBankDesktop
{
    class MakeRequest
    {
        string _address = "http://api.worldbank.org";
        string _format = "&format=json";
        HttpClient _client = new HttpClient();
        JArray _JsonResponse = new JArray();

        public async Task<List<Indicators>> getAllIndicators()
        {
            var section = "/indicators";
            await makeRequest(section, 1);
            await makeRequest(section, (int)_JsonResponse[0]["total"]);
            return JsonConvert.DeserializeObject<List<Indicators>>(_JsonResponse[1].ToString());
        }

        public async Task<List<Countries>> getAllCountries()
        {
            var section = "/countries";
            await makeRequest(section, 1);
            await makeRequest(section, (int)_JsonResponse[0]["total"]);
            return JsonConvert.DeserializeObject<List<Countries>>(_JsonResponse[1].ToString());
        }

        public async Task<List<IndicatorData>> getIndicatorValues(string indicator)
        {
            var section = "/countries/all/indicators/" + indicator;
            await makeRequest(section, 1);
            if (_JsonResponse.Count == 0)
            {
                return new List<IndicatorData>();
            }
            if (_JsonResponse[0]["total"] == null)
            {
                return new List<IndicatorData>();
            }
            await makeRequest(section, (int)_JsonResponse[0]["total"]);
            JArray array;
            try
            {
                array = JArray.Parse(_JsonResponse[1].ToString());
            }
            catch
            {
                return new List<IndicatorData>();
            }
            return JsonConvert.DeserializeObject<List<IndicatorData>>(_JsonResponse[1].ToString(),
                new JsonSerializerSettings
                {
                    Error = delegate(object sender, ErrorEventArgs args)
                    {
                        args.ErrorContext.Handled = true;
                    }
                });
        }

        public async Task makeRequest(string section, int total)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(_address + section + "/?per_page=" + total + _format);
                response.EnsureSuccessStatusCode();
                _JsonResponse = JArray.Parse(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                _JsonResponse = new JArray();
            }
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
