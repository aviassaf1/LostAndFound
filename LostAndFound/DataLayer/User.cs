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
    
    public partial class User
    {
        public User()
        {
            this.Companies = new HashSet<Companies>();
        }
    
        public string UserName { get; set; }
        public string password { get; set; }
        public Nullable<bool> isAdmin { get; set; }
    
        public virtual ICollection<Companies> Companies { get; set; }
    }
}
