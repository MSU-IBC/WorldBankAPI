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
    
    public partial class DIBS_Units
    {
        public DIBS_Units()
        {
            this.DIBS_Fields = new HashSet<DIBS_Fields>();
        }
    
        public int UnitID { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<DIBS_Fields> DIBS_Fields { get; set; }
    }
}
