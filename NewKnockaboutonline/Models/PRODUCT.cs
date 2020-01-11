//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewKnockaboutonline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.ORDER_DETAIL = new HashSet<ORDER_DETAIL>();
        }
    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> UserId { get; set; }
        public int CategoryId { get; set; }
        public Nullable<int> ColorId { get; set; }
        public Nullable<int> StorageId { get; set; }
        public Nullable<System.DateTime> SellStartDate { get; set; }
        public Nullable<System.DateTime> SellEndDate { get; set; }
        public Nullable<int> IsNew { get; set; }
        public Nullable<int> SizeId { get; set; }
        public string ImageBg { get; set; }
        public string ImageSm1 { get; set; }
        public string ImageSm2 { get; set; }
        public string ImageSm3 { get; set; }
        public string ImageSm4 { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        public virtual COLOR COLOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_DETAIL> ORDER_DETAIL { get; set; }
        public virtual STORAGE STORAGE { get; set; }
        public virtual USER USER { get; set; }
        public virtual SIZE SIZE { get; set; }
    }
}
