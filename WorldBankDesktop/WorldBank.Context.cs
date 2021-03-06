﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    
    public partial class GLOBALEDGE_MVCEntities : DbContext
    {
        public GLOBALEDGE_MVCEntities()
            : base("name=GLOBALEDGE_MVCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<DIBS_Fields> DIBS_Fields { get; set; }
        public DbSet<DIBS_Units> DIBS_Units { get; set; }
        public DbSet<Country> Countries { get; set; }
    
        public virtual int DIBS_Field_Insert(string fieldID, string name, string description, Nullable<int> unitID, Nullable<int> categoryID, Nullable<int> sourceID, Nullable<bool> fieldNumeric, Nullable<bool> fieldText)
        {
            var fieldIDParameter = fieldID != null ?
                new ObjectParameter("FieldID", fieldID) :
                new ObjectParameter("FieldID", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var unitIDParameter = unitID.HasValue ?
                new ObjectParameter("UnitID", unitID) :
                new ObjectParameter("UnitID", typeof(int));
    
            var categoryIDParameter = categoryID.HasValue ?
                new ObjectParameter("CategoryID", categoryID) :
                new ObjectParameter("CategoryID", typeof(int));
    
            var sourceIDParameter = sourceID.HasValue ?
                new ObjectParameter("SourceID", sourceID) :
                new ObjectParameter("SourceID", typeof(int));
    
            var fieldNumericParameter = fieldNumeric.HasValue ?
                new ObjectParameter("FieldNumeric", fieldNumeric) :
                new ObjectParameter("FieldNumeric", typeof(bool));
    
            var fieldTextParameter = fieldText.HasValue ?
                new ObjectParameter("FieldText", fieldText) :
                new ObjectParameter("FieldText", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DIBS_Field_Insert", fieldIDParameter, nameParameter, descriptionParameter, unitIDParameter, categoryIDParameter, sourceIDParameter, fieldNumericParameter, fieldTextParameter);
        }
    
        public virtual ObjectResult<DIBS_Insert_Unit_Result> DIBS_Insert_Unit(string unit)
        {
            var unitParameter = unit != null ?
                new ObjectParameter("Unit", unit) :
                new ObjectParameter("Unit", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DIBS_Insert_Unit_Result>("DIBS_Insert_Unit", unitParameter);
        }
    
        public virtual int DIBS_Insert_Data(string fieldID, Nullable<decimal> value, Nullable<int> year, Nullable<int> countryID)
        {
            var fieldIDParameter = fieldID != null ?
                new ObjectParameter("FieldID", fieldID) :
                new ObjectParameter("FieldID", typeof(string));
    
            var valueParameter = value.HasValue ?
                new ObjectParameter("Value", value) :
                new ObjectParameter("Value", typeof(decimal));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var countryIDParameter = countryID.HasValue ?
                new ObjectParameter("CountryID", countryID) :
                new ObjectParameter("CountryID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DIBS_Insert_Data", fieldIDParameter, valueParameter, yearParameter, countryIDParameter);
        }
    }
}
