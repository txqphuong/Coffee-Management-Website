//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCaPhe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NHACC
    {
        public NHACC()
        {
            this.HOADONNHAPs = new HashSet<HOADONNHAP>();
        }
    
        public int MANCC { get; set; }
        public string TENNHACC { get; set; }
        public string SDTNHACCC { get; set; }
        public string DIACHINHACC { get; set; }
    
        public virtual ICollection<HOADONNHAP> HOADONNHAPs { get; set; }
    }
}
