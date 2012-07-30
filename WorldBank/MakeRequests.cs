using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldBank.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using IBC.Database;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace WorldBank
{
    public class MakeRequests
    {
        WorldBankDataContext _dc = new WorldBankDataContext();

        const string _url = "http://api.worldbank.com";
        const string _format = "&format=json";
        db _db = new db("GLOBALEDGE_MVCConnectionString");
        public List<Countries> getAllCountries()
        {
            var output = (makeRequest("/countries/?per_page=1"));
            JArray array = JArray.Parse(output);
            var total = array[0]["total"];
            output = makeRequest("/countries/?per_page=" + total);
            array = JArray.Parse(output);
            var country = JsonConvert.DeserializeObject<List<Countries>>(array[1].ToString());
            _db.ParameterAdd("@iso2code", "");
            _db.ParameterAdd("@NameCIA", "");
            var sqlStr = "UPDATE country SET iso2code=@iso2code WHERE Abbr=@iso2code OR NameCIA=@NameCIA";
            var countryList = new List<Countries>();
            foreach (var i in country)
            {
                _db.ParameterEdit("@iso2code", i.iso2code);
                _db.ParameterEdit("@NameCIA", i.name);
                var ex = _db.ExecuteSql(sqlStr);
                if (ex != null)
                {
                    throw new Exception(ex.Message);
                }
                if (_db.getRowsUpdated() == 0)
                {
                    countryList.Add(new Countries { iso2code = i.iso2code, name = i.name });
                }
            }
            _db.CloseConnection();
            return countryList;
        }

        public List<Indicators> getAllIndicators()
        {
            var output = (makeRequest("/indicators/?per_page=1"));
            JArray array = JArray.Parse(output);
            var total = array[0]["total"];
            output = makeRequest("/indicators/?per_page=" + total);
            array = JArray.Parse(output);
            var indicators = JsonConvert.DeserializeObject<List<Indicators>>(array[1].ToString());

            _db.SetSqlStoredProcedure();
            var indicatorList = new List<Indicators>();
            Exception ex;
            foreach (var i in indicators)
            {
                Match m = Regex.Match(i.name.Trim(), @"(.*)\((.*)\)+$");
                string name = "", unit = "";
                if (m.Groups.Count > 1)
                {
                    name = m.Groups[1].Value;
                    unit = m.Groups[2].Value;
                }
                else
                {
                    name = i.name;
                }
                _db.ParameterAdd("@Unit", unit);

                ex = _db.ExecuteSqlReader("DIBS_Insert_Unit");
                _db.ParameterClearAll();
                if (ex != null)
                {
                    throw new Exception(ex.Message);
                }
                if (_db.Reader.HasRows)
                {
                    _db.Reader.Read();
                    _db.ParameterAdd("@UnitID", (int)_db.Reader["UnitID"]);
                }
                else
                {
                    _db.Reader.NextResult();
                    _db.Reader.Read();
                    _db.ParameterAdd("@UnitID", Convert.ToInt32(_db.Reader["ident"]));
                }
                _db.Reader.Close();

                _db.ParameterAdd("@FieldID", i.id);
                _db.ParameterAdd("@Name", name.Trim());
                _db.ParameterAdd("@Description", i.sourceNote.Trim());
                _db.ParameterAdd("@CategoryID", 117);
                _db.ParameterAdd("@SourceID", 38);
                _db.ParameterAdd("@FieldNumeric", true);
                _db.ParameterAdd("@FieldText", false);
                ex = _db.ExecuteSql("[DIBS_Field_Insert]");
                if (ex != null)
                {
                    throw new Exception(ex.Message);
                }
                if (_db.getRowsUpdated() == 0)
                {
                    indicatorList.Add(new Indicators { id = i.id, name = i.name, sourceNote = i.sourceNote });
                }
                _db.ParameterClearAll();
            }
            _db.CloseConnection();
            return indicatorList;
        }

        public Msg updateIndicator(string indicator)
        {
            indicator = indicator.Replace("~", ".");
            var url = "/countries/all/indicators/";
            var countryList = _dc.Countries.Select(a => new CountriesWithID { name = a.Tag.Title, countryid = a.CountryID, iso2code = a.iso2code });
            JArray array;
            var output = makeRequest(url + indicator + "?per_page=1");
            try
            {
                array = JArray.Parse(output);
            }
            catch(Exception ex)
            {
                var msg = new Msg();
                msg.msg = "BAD JSON";
                _db.ParameterClearAll();
                _db.ParameterAdd("@FieldID", indicator);
                updateField(indicator);
                return msg;
            }
            
            var total = array[0]["total"];

            if (total.ToString() == "0")
            {
                var msg = new Msg();
                msg.msg = "nothing to update";
                _db.ParameterClearAll();
                _db.ParameterAdd("@FieldID", indicator);
                updateField(indicator);
                return msg;
            }
            output = makeRequest(url + indicator + "?per_page=" + total);
            array = JArray.Parse(output);
            try
            {
                var indicators = JsonConvert.DeserializeObject<List<IndicatorData>>(array[1].ToString());
                var notUpdated = new List<IndicatorData>();
                var ex = new Exception();
                _db.ParameterAdd("@value", "");
                _db.ParameterAdd("@year", "");
                _db.ParameterAdd("@countryid", "");
                _db.ParameterAdd("@fieldID", indicator);
                _db.SetSqlStoredProcedure();
                foreach (var i in indicators)
                {
                    if (!i.value.HasValue)
                    {
                        continue;
                    }
                    //throw new Exception(countryList.SingleOrDefault(a => a.iso2code == i.country.id).countryid.ToString());
                    var country = countryList.SingleOrDefault(a => a.iso2code == i.country.id);
                    if (country == null)
                    {
                        continue;
                    }
                    _db.ParameterEdit("@value", i.value.Value);
                    _db.ParameterEdit("@year", i.date);
                    _db.ParameterEdit("@countryid", country.countryid);
                    ex = _db.ExecuteSql("DIBS_Insert_Data");
                    if (ex != null)
                    {
                        throw new Exception(ex.Message);
                    }
                    if (_db.getRowsUpdated() == 0)
                    {
                        notUpdated.Add(i);
                    }

                }
            }
            catch (Exception ex)
            {
                _db.ParameterClearAll();
                _db.ParameterAdd("@FieldID", indicator);
                updateField(indicator);
                return new Msg() { msg = ex.Message };
            }
            updateField(indicator);
            return new Msg() { msg = "Updated" };
        }

        string makeRequest(string apiCall)
        {
            string output = "";
            HttpWebRequest request = WebRequest.Create(_url + apiCall + _format) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    output = new StreamReader(stream).ReadToEnd();
                }
            }
            return output;
        }

        void updateField(string indicator)
        {
            _db.SetSqlText();
            var ex = _db.ExecuteSql("UPDATE DIBS_Fields SET DateDataUpdated = GETDATE() WHERE FieldID=@FieldID");
            if (ex != null)
            {
                throw new Exception(ex.Message);
            }
            _db.ParameterClearAll();
        }
    }

    public class Msg
    {
        public string msg { get; set; }
    }
}

