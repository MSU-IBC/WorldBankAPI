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
    
    public partial class DIBS_Fields
    {
        public string FieldID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitID { get; set; }
        public int CategoryID { get; set; }
        public int SourceID { get; set; }
        public bool FieldNumeric { get; set; }
        public bool FieldText { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<System.DateTime> DateFieldUpdated { get; set; }
        public Nullable<System.DateTime> DateDataUpdated { get; set; }
    
        public virtual DIBS_Units DIBS_Units { get; set; }
    }
}
