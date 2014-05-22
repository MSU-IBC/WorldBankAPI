//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorldBankDesktop
{
    using System;
    using System.Collections.Generic;
    
    public partial class Country
    {
        public int CountryID { get; set; }
        public int TagID { get; set; }
        public int StatusID { get; set; }
        public string Abbr { get; set; }
        public string NameCIA { get; set; }
        public string IntroShort { get; set; }
        public string IntroLong { get; set; }
        public string History { get; set; }
        public string Economy { get; set; }
        public string Government { get; set; }
        public string Capital { get; set; }
        public decimal Capital_Lat { get; set; }
        public decimal Capital_Long { get; set; }
        public string Currency { get; set; }
        public string CurrencyCode { get; set; }
        public string CallingCode { get; set; }
        public string Voltage { get; set; }
        public string ChiefOfState { get; set; }
        public string HeadOfGov { get; set; }
        public string USEmbassy { get; set; }
        public string MainLanguages { get; set; }
        public string MainReligions { get; set; }
        public decimal zoom { get; set; }
        public decimal zoom_x { get; set; }
        public decimal zoom_y { get; set; }
        public string iso2code { get; set; }
        public System.DateTime LastReviewed { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Unrest { get; set; }
        public bool UnrestApproved { get; set; }
    }
}