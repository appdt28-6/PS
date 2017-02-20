using PSAppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PSAppWeb.Controllers
{
    public class SetVisitaController : Controller
    {
        // GET: SetVisitInstaller
        private AppDTEntities db = new AppDTEntities();
        public JsonResult Index()
        {

            int valor = Convert.ToInt32(Request.QueryString["inst_id"]);
            //TimeZoneMX horamx = new TimeZoneMX();
            var fechac = DateTime.Now.ToString("yyyy-MM-dd");
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = fechac.ToString().Split(delimiterChars);
            var dateIni = words[0] + " 00:00";
            var dateEnd = words[0] + " 23:59";

            var visitas = db.vis_VISITA_CUSTOMER.Where(v => String.Compare(v.visi_date, dateIni) > 0 && String.Compare(v.visi_date, dateEnd) < 0 && v.inst_id== valor);
            //var visitas = db.vis_VISITA_CUSTOMER;
            return Json(visitas, JsonRequestBehavior.AllowGet);
        }
    }
}