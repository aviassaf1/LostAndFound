//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class LostItems
    {
        public int itemID { get; set; }
        public string companyName { get; set; }
        public string photoLocation { get; set; }
        public string colors { get; set; }
        public string itemType { get; set; }
        public Nullable<System.DateTime> lostDate { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public Nullable<bool> delivered { get; set; }
    
        public virtual CompanyItems CompanyItems { get; set; }
    }
}
