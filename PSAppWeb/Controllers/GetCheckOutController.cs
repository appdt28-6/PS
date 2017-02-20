using PSAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSAppWeb.Controllers
{
    public class GetCheckOutController : Controller
    {
        // GET: GetCheckOut
        public ActionResult Index()
        {
            int inst_id = Convert.ToInt32(Request.QueryString["inst_id"]);
            int visitid = Convert.ToInt32(Request.QueryString["visit_id"]);
            int custid = Convert.ToInt32(Request.QueryString["cust_id"]);
            string lat = Convert.ToString(Request.QueryString["lat"]);
            string lon = Convert.ToString(Request.QueryString["lon"]);
            string hr = Convert.ToString(Request.QueryString["hr"]);
            string fcha = Convert.ToString(Request.QueryString["fcha"]);
            int emo = Convert.ToInt32(Request.QueryString["emo"]);

            var succes = "";

            using (AppDTEntities db = new AppDTEntities())
            {
                try
                {
                   
                    var result = from r in db.VISITA_REGISTRO where r.visi_id == visitid select r;
                    VISITA_REGISTRO vISITA_REGISTRO = result.First();
                    vISITA_REGISTRO.reg_end =hr;
                    vISITA_REGISTRO.reg_emo = emo;
                    db.SaveChanges();
                    //status
                    var result2 = from r in db.VISITA_ASSIGN where r.visi_id == visitid select r;
                    VISITA_ASSIGN vISITA_ASSIGN = result2.First();
                    vISITA_ASSIGN.visi_status = 2;
                    db.SaveChanges();

                    succes = "Ok";
                }
                catch (Exception e)
                {
                    succes = "NoOk";
                }
            }

            return Json(new { succes }, JsonRequestBehavior.AllowGet);
        }
    }
}