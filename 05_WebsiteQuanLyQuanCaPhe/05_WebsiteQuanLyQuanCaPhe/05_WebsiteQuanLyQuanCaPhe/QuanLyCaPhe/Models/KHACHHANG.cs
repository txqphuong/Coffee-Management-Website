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
    
    public partial class KHACHHANG
    {
        public KHACHHANG()
        {
            this.HOADONs = new HashSet<HOADON>();
        }
    
        public int MAKH { get; set; }
        public string TENNKH { get; set; }
        public string PHAI { get; set; }
        public string SDTKH { get; set; }
        public string TENTAIKHOAN { get; set; }
        public string MATKHAUTK { get; set; }
        public string DIACHIKH { get; set; }
        public string EMAIL { get; set; }
    
        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}