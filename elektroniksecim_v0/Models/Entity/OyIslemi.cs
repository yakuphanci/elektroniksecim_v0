//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace elektroniksecim_v0.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class OyIslemi
    {
        public long oyID { get; set; }
        public long secimID { get; set; }
        public long secmenID { get; set; }
        public long adayID { get; set; }
        public Nullable<System.DateTimeOffset> oyVermeTarihi { get; set; }
    
        public virtual Aday Aday { get; set; }
        public virtual Secim Secim { get; set; }
        public virtual Secmen Secmen { get; set; }
    }
}
