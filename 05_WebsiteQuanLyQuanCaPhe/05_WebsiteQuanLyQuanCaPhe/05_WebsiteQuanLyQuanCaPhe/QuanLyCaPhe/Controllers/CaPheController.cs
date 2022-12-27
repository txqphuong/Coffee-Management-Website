using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCaPhe.Models;

namespace QuanLyCaPhe.Controllers
{
    public class CaPheController : Controller
    {
        //
        // GET: /CaPhe/
        QLCAPHE_WEBEntities4 db = new QLCAPHE_WEBEntities4();

        #region trang main, client, gio hang
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult CustomerLogin()
        { 
            return View();
        }
        //form dang nhap khach hang
        [HttpPost]
        public ActionResult CustomerLogin(User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    return View();
                }
                TAIKHOAN temp = db.TAIKHOANs.Find(user.Username);

                if (temp == null || temp.MatKhau.Trim() != user.Password)
                {
                    ViewBag.Error = "Wrong password";
                    return View();
                }
                Session["user"] = new User(user);
                return RedirectToAction("ShowSanPham", "CaPhe");

            }
            return View();
        }

        public ActionResult ManagerLogin()
        {
            return View();
        }
        //form dang nhap admin
        [HttpPost]
        public ActionResult ManagerLogin(User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    return View();
                }
                TAIKHOAN temp = db.TAIKHOANs.Find(user.Username);

                if (temp == null || temp.MatKhau.Trim() != user.Password)
                {
                    ViewBag.Error = "Wrong password";
                    return View();
                }
                Session["user"] = new User(user);
                return RedirectToAction("TabTongQuan", "CaPhe");

            }
            return View();
        }

        //hien thi trang chu
        public ActionResult showMainPage()
        {
            return View();
        }

        //hien thi danh sach tat ca mon an
        public ActionResult ShowSanPham()
        {
            List<SANPHAM> lsp = db.SANPHAMs.ToList();
            return View(lsp);
        }

        //log out -> quay lai trang san pham
        public ActionResult LogOut()
        {
            Session.Abandon();
            return Redirect("ShowSanPham");
        }
        //lay gio hang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lGH = Session["giohang"] as List<GioHang>;
            if (lGH == null)
            {
                lGH = new List<GioHang>();
                Session["giohang"] = lGH;
            }
            return lGH;
        }

        //them gio hang
        public ActionResult ThemGioHang(int maSP, string url)
        {
            List<GioHang> lst = LayGioHang();
            GioHang gh = lst.Find(t => t.MaSanPham.Equals(maSP));
            if (gh == null)
            {
                gh = new GioHang(maSP);
                lst.Add(gh);
            }
            else
            {
                gh.SoLuong++;
                gh.ThanhTien = gh.SoLuong * gh.Gia;
            }
            return Redirect(url);
        }

        //xoa gio hang
        public ActionResult XoaGioHang(int ma)
        {
            List<GioHang> lst = LayGioHang();
            GioHang gh = lst.Single(s => s.MaSanPham == ma);
            if (gh != null)
            {
                lst.RemoveAll(s => s.MaSanPham == ma);
                return RedirectToAction("ShowSanPham", "CaPhe");
            }
            if (lst.Count == 0)
            {
                return RedirectToAction("ShowSanPham", "CaPhe");
            }
            return RedirectToAction("ShowSanPham", "CaPhe");
        }

        //gio hang
        public ActionResult GioHang()
        {
            List<GioHang> lGH = Session["giohang"] as List<GioHang>;
            if (lGH == null)
            {
                return RedirectToAction("NotFound");
            }
            ViewBag.TongThanhTien = lGH.Sum(t => t.ThanhTien);
            ViewBag.TongSL = lGH.Sum(t => t.SoLuong);
            return View(lGH);
        }

        public ActionResult GioHangPartial2()
        {
            List<GioHang> lGH = Session["giohang"] as List<GioHang>;
            if (lGH == null)
            {
                return PartialView();
            }
            ViewBag.TongThanhTien = lGH.Sum(t => t.ThanhTien);
            ViewBag.TongSL = lGH.Sum(t => t.SoLuong);
            return PartialView(lGH);
        }

        public ActionResult GioHangPartial()
        {
            List<GioHang> lGH = Session["giohang"] as List<GioHang>;
            if (lGH == null)
                ViewBag.soLuong = 0;
            else
                ViewBag.soLuong = lGH.Sum(t => t.SoLuong);
            return PartialView();
        }
        //Tìm kiếm
        public ActionResult TimSanPham(string txt_Search)
        {
            var list = db.SANPHAMs.Where(sp => sp.TENSP.Contains(txt_Search) == true).ToList();
            if (list.Count == 0)
            {
                ViewBag.TB = "Không có sản phẩm nào";
            }
            return View(list);
        }
        //Đăng ký
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh, FormCollection f)
        {
            //Gán các gia trị người dùng nhập từ form cho các biến => kiểm tra không nhập xuất ra lỗi
            var hoTen = f["HotenKH"];
            var tenTaiKhoan = f["TenTK"];
            var matKhau = f["MatKhau"];
            var dienThoai = f["DienThoai"];
            var diaChi = f["DiaChi"];
            var email = f["Email"];
            if (String.IsNullOrEmpty(hoTen))
            {
                ViewData["Loi1"] = "Họ tên không được bỏ trống";
            }
            if (String.IsNullOrEmpty(tenTaiKhoan))
            {
                ViewData["Loi2"] = "Tên đăng nhập không được bỏ trống";
            }
            if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi3"] = "Mật khẩu không được bỏ trống";
            }
            if (String.IsNullOrEmpty(dienThoai))
            {
                ViewData["Loi4"] = "Số điện thoại không được bỏ trống";
            }
            if (String.IsNullOrEmpty(diaChi))
            {
                ViewData["Loi5"] = "Ngày sinh không được bỏ trống";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Email không được bỏ trống";
            }
            if (!String.IsNullOrEmpty(hoTen) && !String.IsNullOrEmpty(tenTaiKhoan) && !String.IsNullOrEmpty(matKhau) && !String.IsNullOrEmpty(dienThoai) && !String.IsNullOrEmpty(diaChi) && !String.IsNullOrEmpty(email))
            {
                kh.TENNKH = hoTen;
                kh.TENTAIKHOAN = tenTaiKhoan;
                kh.MATKHAUTK = matKhau;
                kh.SDTKH = dienThoai;
                kh.DIACHIKH = diaChi;
                kh.EMAIL = email;
                //Ghi xuống CSDL
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("ShowSanPham", "CaPhe");
            }
            return View();
        }
        //Show tất cả khuyến mãi
        public ActionResult ShowKhuyenMai()
        {
            var list = db.KHUYENMAIs.ToList();
            return View(list);
        }
        #endregion

        #region trang manager
        //hien thi layout manager
        public ActionResult Index2()
        {
            return View();
        }        

        #region tab tong quan
        //hien thi tab hoa don
        public ActionResult TabTongQuan()
        {
            ViewBag.NV = db.NHANVIENs.Count();
            ViewBag.HD = db.HOADONs.Count();
            return View();
        }
        #endregion

        #region tab thuc don
        //thuc don hien thi tat ca san pham
        public ActionResult ThucDon()
        {
            var listSanPham = db.SANPHAMs.ToList();
            ViewBag.SL = listSanPham.Count();
            return View(listSanPham);
        }

        //xoa san pham
        public ActionResult deleteconfim(int id)
        {
            SANPHAM sp = db.SANPHAMs.Single(s => s.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        [HttpPost, ActionName("deleteconfim")]
        public ActionResult delete2(int id)
        {
            SANPHAM sp = db.SANPHAMs.Single(s => s.MASP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            db.Entry(sp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ThucDon", "CaPhe");
        }

        //cap nhat san pham
        public ActionResult UpDate(int id)
        {
            SANPHAM sp = db.SANPHAMs.Single(s => s.MASP == id);
            return View(sp);
        }
        [HttpPost]
        public ActionResult UpDate(SANPHAM sp)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Attach(sp);
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThucDon", "CaPhe");
            }
            return View(sp);
        }

        //them san pham
        public ActionResult Insert()
        {
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP");
            return View();
        }
        [HttpPost]
        public ActionResult Insert(SANPHAM sp)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
                return RedirectToAction("ThucDon", "CaPhe");
            }
            return View(sp);
        }
        #endregion

        #region tab hoa don
        //hien thi hoa don
        public ActionResult HoaDon()
        {
            var listHoaDon = db.HOADONs.Include("KHACHHANG").Include("NHANVIEN").ToList();
            ViewBag.SL = listHoaDon.Count();
            return View(listHoaDon);
        }

        //tao hoa don
        public ActionResult Create()
        {
            ViewBag.MAHD = new SelectList(db.HOADONs, "MAHD");
            return View();
        }
        [HttpPost]
        public ActionResult Create(HOADON hd)
        {
            if (ModelState.IsValid)
            {
                db.HOADONs.Add(hd);
                db.SaveChanges();
                return RedirectToAction("HoaDon", "CaPhe");
            }
            return View(hd);
        }

        //Tao chi tiet hoa don
        public ActionResult CreateCTHD()
        {
            ViewBag.MACTHD = new SelectList(db.CHITIETHDs, "MAHD");
            return View();
        }
        [HttpPost]
        public ActionResult CreateCTHD(CHITIETHD cthd)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETHDs.Add(cthd);
                db.SaveChanges();
                return RedirectToAction("HoaDon", "CaPhe");
            }
            return View(cthd);
        }

        //xem chi tiet hoa don
        public ActionResult XemChiTietHoaDon(int id)
        {
            var xem = db.CHITIETHDs.Where(s => s.MAHD == id).ToList();
            return View(xem);
        }

        //xoa chi tiet hoa don
        public ActionResult deleteconfimCTHD(int id)
        {
            //CHITIETHD hd = db.CHITIETHDs.Single(s => s.MASP == id);
            //if (hd == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(hd);

            try
            {
                CHITIETHD hd = db.CHITIETHDs.Single(s => s.MASP == id);
                db.CHITIETHDs.Remove(hd);
                db.SaveChanges();//luu du lieu sau khi da xoa nhan vien
            }
            catch
            {
                Session["TB"] = "Cannot delete this bill";
            }
            return RedirectToAction("HoaDon", "CaPhe");
        }
        //[HttpPost, ActionName("deleteconfimCTHD")]
        //public ActionResult delete2CTHD(int id)
        //{
        //    CHITIETHD hd = db.CHITIETHDs.Single(s => s.MASP == id);
        //    if (hd == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.Entry(hd).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();
        //    return RedirectToAction("HoaDon", "CaPhe");
        //}

        //xem chi tiet san pham
        public ActionResult XemChiTietSanPham(int id)
        {
            SANPHAM xem = db.SANPHAMs.Single(s => s.MASP == id);
            return View(xem);
        }

        //cap nhat chi tiet hoa don
        public ActionResult UpDateCTHD(int id)
        {
            CHITIETHD ct = db.CHITIETHDs.Single(s => s.MASP == id);
            return View(ct);
        }
        [HttpPost]
        public ActionResult UpDateCTHD(CHITIETHD ct)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETHDs.Attach(ct);
                db.Entry(ct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HoaDon", "CaPhe");
            }
            return View(ct);
        }
        #endregion

        #region tab khach hang

        //show tat ca khach hang co trong database
        public ActionResult ShowAllKH()
        {
            return View(db.KHACHHANGs.ToList());
        }

        //Them moi khach hang
        public ActionResult AddKH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddKH(KHACHHANG Kh)
        {
            KHACHHANG KH = db.KHACHHANGs.Find(Kh.MAKH);
            if (KH != null)
            {
                return Content("Mã khách hàng đã tồn tại");
            }
            else
            {
                db.KHACHHANGs.Add(Kh);
                db.SaveChanges();
                return RedirectToAction("ShowAllKH", "CaPhe");
            }
        }

        //xoa khach hang
        [HttpGet]
        public ActionResult DeleteKH(int id)
        {
            try
            {
                KHACHHANG kh = db.KHACHHANGs.Find(id);
                db.KHACHHANGs.Remove(kh);
                db.SaveChanges();//luu du lieu sau khi da xoa nhan vien
            }
            catch
            {
                Session["TB"] = "Cannot delete this customer";
            }
            return RedirectToAction("ShowAllKH", "CaPhe");
        }

        //[HttpPost, ActionName("DeleteKH")]
        //public ActionResult DeleteEmpConfirm(int id)
        //{
        //    KHACHHANG KH = db.KHACHHANGs.Single(d => d.MAKH == id);

        //    if (KH == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.Entry(KH).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();
        //    return RedirectToAction("ShowAllKH", "CaPhe");
        //}

        //sua khach hang

        public ActionResult EditKH(int id)
        {
            KHACHHANG KH = db.KHACHHANGs.Find(id);
            return View(KH);
        }
        //lay du lieu da chinh sua o web xuong database 
        [HttpPost]
        public ActionResult EditKH(KHACHHANG Kh)
        {
            db.Entry(Kh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();//luu du lieu da sua cua nhan vien
            return RedirectToAction("ShowAllKH", "CaPhe");
        }
        #endregion

        #region tab nhan vien
        //show tat ca nhan vien
        public ActionResult ShowAllNV()
        {
            return View(db.NHANVIENs.ToList());
        }

        //them 1 nhan vien
        public ActionResult AddNV()
        {
            return View();
        }

        //lay du lieu da them xuong database
        [HttpPost]
        public ActionResult AddNV(NHANVIEN Nv)
        {
            NHANVIEN NV = db.NHANVIENs.Find(Nv.MANV);
            if (NV != null)//Ktr ma nhan vien them da trung voi ma nhan vien da co trong database
            {
                return Content("Mã nhân viên đã tồn tại");
            }
            else //Ktr khong trung them moi nhan vien vao database
            {
                db.NHANVIENs.Add(Nv);
                db.SaveChanges();//luu thay doi sau khi da them moi
                return RedirectToAction("ShowAllNV", "CaPhe");
            }
        }

        //Xoa 1 nhan vien muon xoa
        public ActionResult DeleteNV(int Ma)
        {
            try
            {
                NHANVIEN NV = db.NHANVIENs.Find(Ma);
                db.NHANVIENs.Remove(NV);
                db.SaveChanges();//luu du lieu sau khi da xoa nhan vien
            }
            catch 
            {
                Session["TB"] = "Cannot delete this employee";
            }
            return RedirectToAction("ShowAllNV", "CaPhe");
        }

        //chinh sua thong tin cua 1 nhan vien
        public ActionResult EditNV(int id)
        {
            NHANVIEN NV = db.NHANVIENs.Find(id);
            return View(NV);
        }

        //lay du lieu da chinh sua o web xuong database 
        [HttpPost]
        public ActionResult EditNV(NHANVIEN Nv)
        {
            db.Entry(Nv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();//luu du lieu da sua cua nhan vien
            return RedirectToAction("ShowAllNV", "CaPhe");
        }
        #endregion

        #region tab khuyen mai
        //show tat ca KM trong database
        public ActionResult ShowAllKM()
        {
            return View(db.KHUYENMAIs.ToList());
        }

        public ActionResult AddKM()//them khuyen mai
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddKM(KHUYENMAI Km)
        {
            KHUYENMAI KM = db.KHUYENMAIs.Find(Km.MaKM);
            if (KM != null)
            {
                return Content("Mã khuyến mãi đã tồn tại");
            }
            else
            {
                db.KHUYENMAIs.Add(Km);
                db.SaveChanges();
                return RedirectToAction("ShowAllKM", "CaPhe");
            }
        }

        //Xoa 1 km
        public ActionResult DeleteKM(int Makm)
        {
            try
            {
                KHUYENMAI KM = db.KHUYENMAIs.Find(Makm);
                db.KHUYENMAIs.Remove(KM);
                db.SaveChanges();
            }
            catch
            {
                Session["TB"] = "Cannot delete this event";
            }
            return RedirectToAction("ShowAllKM", "CaPhe");
        }

        //sua doi thong tin KM
        public ActionResult EditKM(int ID)
        {
            KHUYENMAI KM = db.KHUYENMAIs.Find(ID);
            return View();
        }
        [HttpPost]
        public ActionResult EditKM(KHUYENMAI Km)
        {
            db.Entry(Km).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowAllKM", "CaPhe");
        }
        #endregion

        #region tab nguyen lieu
        //show tat ca NL trong database
        public ActionResult ShowAllNL()
        {
            return View(db.NGUYENLIEUx.ToList());
        }

        public ActionResult AddNL()//them nguyen lieu
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNL(NGUYENLIEU Nl)
        {
            NGUYENLIEU NL = db.NGUYENLIEUx.Find(Nl.MaNL);
            if (NL != null)
            {
                return Content("Mã nguyên liệu này đã tồn tại");
            }
            else
            {
                db.NGUYENLIEUx.Add(Nl);
                db.SaveChanges();
                return RedirectToAction("ShowAllNL", "CaPhe");
            }
        }

        //Xoa 1 NL
        public ActionResult DeleteNL(int MaNl)
        {
            try
            {
                NGUYENLIEU NL = db.NGUYENLIEUx.Find(MaNl);
                db.NGUYENLIEUx.Remove(NL);
                db.SaveChanges();
            }
            catch
            {
                Session["TB"] = "Cannot delete this category";
            }
            
            return RedirectToAction("ShowAllNL", "CaPhe");
        }

        //sua doi thong tin NL
        public ActionResult EditNL(int ID)
        {
            NGUYENLIEU NL = db.NGUYENLIEUx.Find(ID);
            return View();
        }
        [HttpPost]
        public ActionResult EditNL(NGUYENLIEU Kl)
        {
            db.Entry(Kl).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowAllNL", "CaPhe");
        }
        #endregion

        #endregion
    }
}