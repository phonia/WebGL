using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGLWithDataBase.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            String s1 = String.Empty;
            String s2 = String.Empty;

            String connectionString = "data source=192.168.10.3;initial catalog=GZDTProject;user id=sa;password=Sa123;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from model3Ds where Id=2185";
                SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); ;
                if (sdr.Read())
                {
                    s1 = sdr["Path"].ToString();
                    s2 = sdr["Section"].ToString();
                }
            }
            string points = s1.Replace(' ', ',');
            if (points.LastIndexOf(',') == points.Length - 1)
            {
                points = points.Substring(0, points.Length - 1);
            }
            TempData temp = new TempData() { Points = points, Indexs = s2 };
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

    }

    public class TempData
    {
        public String Points { get; set; }
        public String Indexs { get; set; }
    }
}
