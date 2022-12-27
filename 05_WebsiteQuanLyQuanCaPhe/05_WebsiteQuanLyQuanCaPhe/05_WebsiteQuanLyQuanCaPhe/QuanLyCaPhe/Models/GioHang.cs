using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyCaPhe.Models;

namespace QuanLyCaPhe.Models
{
    public class GioHang
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public double Gia { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }

        public GioHang(int maSP)
        {
            QLCAPHE_WEBEntities4 db = new QLCAPHE_WEBEntities4();
            SANPHAM x = db.SANPHAMs.Single(t=>t.MASP.Equals(maSP));
            MaSanPham = maSP;
            TenSanPham = x.TENSP;
            Gia = double.Parse(x.GIABAN.ToString());
            HinhAnh = x.HINHANH;
            SoLuong = 1;
            ThanhTien = SoLuong * Gia;
        }
    }
}