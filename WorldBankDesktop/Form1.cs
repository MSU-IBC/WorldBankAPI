using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldBank.Models;

namespace WorldBankDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerAsync();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var mr = new MakeRequest();
            var indicators = mr.getAllIndicators();
            List<Indicators> ind = await indicators;
            using (var _db = new GLOBALEDGE_MVCEntities())
            {
                foreach (var i in ind)
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
                    var unitID = _db.DIBS_Insert_Unit(unit).First().UnitID;
                    _db.SaveChanges();
                    _db.DIBS_Field_Insert(i.id, i.name.Trim(), i.sourceNote.Trim(), unitID, 117, 38, true, false);
                    _db.SaveChanges();
                    textBox1.Invoke(new UpdateTextBox(UpdateText), i.name.Trim());
                }
            }
            mr.Dispose();
        }

        private void UpdateText(string text)
        {
            textBox1.AppendText(text + Environment.NewLine);
        }

        private delegate void UpdateTextBox(string text);

        private async void btnValues_Click(object sender, EventArgs e)
        {
            var mr = new MakeRequest();
            //await getCountries();
            using (var _db = new GLOBALEDGE_MVCEntities())
            {
                var countryList = _db.Countries.Select(a=>new CountriesWithID{countryid=a.CountryID, iso2code=a.iso2code, name=a.NameCIA});
                var indicatorList = _db.DIBS_Fields.Where(a => a.SourceID == 38 && a.DateDataUpdated < dateTimePicker.Value).Select(a => a.FieldID).ToList();
                foreach (var indicator in indicatorList)
                {
                    textBox1.Invoke(new UpdateTextBox(UpdateText), indicator + " is being updated");
                    List<IndicatorData> indicatorData = await mr.getIndicatorValues(indicator);
                    foreach (var i in indicatorData)
                    {
                        if (!i.value.HasValue)
                        {
                            continue;
                        }
                        // Checks to see if it's in our country list.
                        var country = countryList.FirstOrDefault(a => a.iso2code == i.country.id);
                        if (country == null)
                        {
                            continue;
                        }
                        _db.DIBS_Insert_Data(indicator, i.value, i.date, country.countryid);
                    }
                    updateField(indicator);
                }
            }
        }

        void updateField(string indicator)
        {
            using (var _db = new GLOBALEDGE_MVCEntities())
            {
                var i = _db.DIBS_Fields.FirstOrDefault(a => a.FieldID == indicator);
                i.DateDataUpdated = DateTime.Now;
                _db.SaveChanges();
            }
        }

        private async Task getCountries()
        {
            var mr = new MakeRequest();
            var countries = mr.getAllCountries();
            List<Countries> country = await countries;
            using (var _db = new GLOBALEDGE_MVCEntities())
            {
                foreach (var i in country)
                {
                    textBox1.Invoke(new UpdateTextBox(UpdateText), i.name + " ...");
                    var c = _db.Countries.FirstOrDefault(a => a.Abbr == i.iso2code || a.NameCIA == i.name);
                    if (c == null)
                    {
                        continue;
                    }
                    c.iso2code = i.iso2code;
                    _db.SaveChanges();
                    textBox1.Invoke(new UpdateTextBox(UpdateText), i.name + " Updated");
                }
            }
        }
    }
}
