﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WorldBank.Models
{
    public class Indicators
    {
        public string id;
        public string name;
        public string sourceNote;
    }

    public class IndicatorData
    {
        public Value indicator { get; set; }
        public Value country { get; set; }
        public double? value { get; set; }
        [JsonProperty("decimal")]
        public int dec { get; set; }
        public int date { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string value { get; set; }
    }
}