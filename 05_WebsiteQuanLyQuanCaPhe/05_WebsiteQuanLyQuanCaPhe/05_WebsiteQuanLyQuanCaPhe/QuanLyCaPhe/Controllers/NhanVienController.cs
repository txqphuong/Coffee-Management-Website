using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QuanLyCaPhe.Models;


namespace QuanLyCaPhe.Controllers
{
    public class NhanVienController : Controller
    {
           apiNhanVienController apiNhanVien = new apiNhanVienController();
           public ActionResult Show5NhanVien()
            {
                List<NHANVIEN> list = apiNhanVien.lay5NhanVien();
                return View(list);
            }
	}
}