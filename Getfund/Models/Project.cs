//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Getfund.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            this.Comments = new HashSet<Comment>();
            this.Donations = new HashSet<Donation>();
        }
    
        public int PId { get; set; }
        public Nullable<int> ID { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string VideoLink { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string ProjectImage1 { get; set; }
        public Nullable<int> Likes { get; set; }
        public Nullable<double> MoneyRaised { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation> Donations { get; set; }
        public virtual GUser GUser { get; set; }
    }
}
