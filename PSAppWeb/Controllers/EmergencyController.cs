using PSAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSAppWeb.Controllers
{
    public class EmergencyController : Controller
    {
        // GET: Emergency
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

            using (AppDTEntities objDataContext = new AppDTEntities())
            {
                try
                {
                    VISITA_REGISTRO vISITA = new VISITA_REGISTRO();
                    // fields to be insert
                    vISITA.inst_id = inst_id;
                    vISITA.reg_lat = lat;
                    vISITA.reg_lon = lon;
                    vISITA.cust_id = custid;
                    vISITA.reg_date = fcha;
                    vISITA.reg_ini = hr;
                    vISITA.reg_end = hr;
                    vISITA.visi_id = 100;
                    vISITA.reg_status = 8;
                    vISITA.reg_emo = 0;
                    objDataContext.VISITA_REGISTRO.Add(vISITA);

                    objDataContext.SaveChanges();

                    var result = from r in objDataContext.VISITA_ASSIGN where r.visi_id == visitid select r;

                    // Get the first record from the result
                    VISITA_ASSIGN vISITA_ASSIGN = result.First();

                    // Update the product name
                    vISITA_ASSIGN.visi_status = 1;

                    objDataContext.SaveChanges();


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