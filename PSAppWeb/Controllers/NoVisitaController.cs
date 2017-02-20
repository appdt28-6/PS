using PSAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSAppWeb.Controllers
{
    public class NoVisitaController : Controller
    {
        // GET: NoVisita
        public ActionResult Index()
        {
            int inst_id = Convert.ToInt32(Request.QueryString["inst_id"]);
            int visitid = Convert.ToInt32(Request.QueryString["visit_id"]);
            string hr = Convert.ToString(Request.QueryString["hr"]);
            string fcha = Convert.ToString(Request.QueryString["fcha"]);
            string reason = Convert.ToString(Request.QueryString["reason"]);
            string lat = Convert.ToString(Request.QueryString["lat"]);
            string lon = Convert.ToString(Request.QueryString["lon"]);
            int custid = Convert.ToInt32(Request.QueryString["cust_id"]);

            var succes = "";

            using (AppDTEntities objDataContext = new AppDTEntities())
            {
                try
                {
                    NOVISITA vISITA = new NOVISITA();
                    // fields to be insert
                    vISITA.inst_id = inst_id;
                    vISITA.vist_id = visitid;
                    vISITA.novi_date = fcha;
                    vISITA.novi_hora = hr;
                    vISITA.novi_reason = reason;
                    objDataContext.NOVISITA.Add(vISITA);

                    objDataContext.SaveChanges();

                    VISITA_REGISTRO vISITA2 = new VISITA_REGISTRO();
                    // fields to be insert
                    vISITA2.inst_id = inst_id;
                    vISITA2.reg_lat = lat;
                    vISITA2.reg_lon = lon;
                    vISITA2.cust_id = custid;
                    vISITA2.reg_date = fcha;
                    vISITA2.reg_ini = hr;
                    vISITA2.reg_end = hr;
                    vISITA2.visi_id = visitid;
                    vISITA2.reg_status = 3;
                    vISITA2.reg_emo = 0;
                    objDataContext.VISITA_REGISTRO.Add(vISITA2);

                    objDataContext.SaveChanges();

                    var result = from r in objDataContext.VISITA_ASSIGN where r.visi_id == visitid select r;

                    // Get the first record from the result
                    VISITA_ASSIGN vISITA_ASSIGN = result.First();

                    // Update the product name
                    vISITA_ASSIGN.visi_status = 3;

                    objDataContext.SaveChanges();

                    succes = "Ok";
                }
                catch (Exception e)
                {
                    succes = "NoOk";
                }

                return Json(new { succes }, JsonRequestBehavior.AllowGet);
        }
    }
    }
}