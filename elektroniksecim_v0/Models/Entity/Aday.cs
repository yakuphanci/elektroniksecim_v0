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
    
    public partial class Aday
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aday()
        {
            this.OyIslemi = new HashSet<OyIslemi>();
        }
    
        public long adayID { get; set; }
        public string adayAdi { get; set; }
        public long secimID { get; set; }
        public long aldigiOy { get; set; }
    
        public virtual Secim Secim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OyIslemi> OyIslemi { get; set; }
    }
}