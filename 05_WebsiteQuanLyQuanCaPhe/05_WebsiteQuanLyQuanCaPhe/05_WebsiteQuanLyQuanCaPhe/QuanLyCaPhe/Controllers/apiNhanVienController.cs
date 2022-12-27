using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuanLyCaPhe.Models;

namespace QuanLyCaPhe.Controllers
{
    public class apiNhanVienController : ApiController
    {
        QLCAPHE_WEBEntities4 db = new QLCAPHE_WEBEntities4();
        public List<NHANVIEN> lay5NhanVien()
        {
            return db.NHANVIENs.Take(5).ToList();
        }
    }
}
