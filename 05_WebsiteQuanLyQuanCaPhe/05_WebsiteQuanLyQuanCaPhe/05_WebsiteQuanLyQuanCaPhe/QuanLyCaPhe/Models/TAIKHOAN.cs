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
    
    public partial class TAIKHOAN
    {
        public Nullable<int> MANV { get; set; }
        public string TenNguoiDung { get; set; }
        public string MatKhau { get; set; }
    
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
