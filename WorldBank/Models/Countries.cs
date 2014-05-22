using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace WorldBank.Models
{
    public class Countries
    {
        public string name { get; set; }
        public string iso2code { get; set; }
    }

    public class CountriesWithID
    {
        public string name { get; set; }
        public string iso2code { get; set; }
        public int countryid { get; set; }
    }
}