﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PSAppWeb.Models;

namespace PSAppWeb.Controllers
{
    public class VisitAssignController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult VisitAssign()
        {
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        public ActionResult GetCustomer()
        {

            var cust = db.CUSTOMER.ToList();

            return Json(cust, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetInstaller()
        {

            var cust = db.INSTRUCTOR.ToList();

            return Json(cust, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VISITA_ASSIGN_Read([DataSourceRequest]DataSourceRequest request)
        {
            string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = date.Split(delimiterChars);
            var dateIni = words[0] + " 00:00";
            var dateEnd = words[0] + " 23:59";
            IQueryable<vis_Assigned_Visit> visita_assign = db.vis_Assigned_Visit.Where(x => String.Compare(x.visi_date, dateIni) > 0 && String.Compare(x.visi_date, dateEnd) < 0);
            DataSourceResult result = visita_assign.ToDataSourceResult(request, vISITA_ASSIGN => new {
                visi_id = vISITA_ASSIGN.visi_id,
                cust_id = vISITA_ASSIGN.cust_id,
                cust_name = vISITA_ASSIGN.cust_name,
                inst_id = vISITA_ASSIGN.inst_id,
                inst_name = vISITA_ASSIGN.inst_name,
                visi_date = vISITA_ASSIGN.visi_date,
                visi_op = vISITA_ASSIGN.visi_op,
                visi_status = vISITA_ASSIGN.visi_status
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VISITA_ASSIGN_Create([DataSourceRequest]DataSourceRequest request, VISITA_ASSIGN vISITA_ASSIGN)
        {
            DateTime now = DateTime.Now;
            now.ToString("yyyy-MM-dd HH:mm:ss"); //Outputs 2014-04-08 12:50:35
            now.ToString("HH:mm:ss");
            var fecha = vISITA_ASSIGN.visi_date.ToString();

            if (ModelState.IsValid)
            {
                var entity = new VISITA_ASSIGN
                {
                    inst_id = vISITA_ASSIGN.inst_id,
                    cust_id = vISITA_ASSIGN.cust_id,
                    visi_date = vISITA_ASSIGN.visi_date,
                    visi_op = vISITA_ASSIGN.visi_op,
                    visi_status = 0,
                    visi_hora = vISITA_ASSIGN.visi_hora,
                    visi_km =25
                };

                db.VISITA_ASSIGN.Add(entity);
                db.SaveChanges();
                vISITA_ASSIGN.visi_id = entity.visi_id;
            }

            return Json(new[] { vISITA_ASSIGN }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VISITA_ASSIGN_Update([DataSourceRequest]DataSourceRequest request, VISITA_ASSIGN vISITA_ASSIGN)
        {
            if (ModelState.IsValid)
            {
                var entity = new VISITA_ASSIGN
                {
                    inst_id = vISITA_ASSIGN.inst_id,
                    cust_id = vISITA_ASSIGN.cust_id,
                    visi_id = vISITA_ASSIGN.visi_id,
                    visi_date = vISITA_ASSIGN.visi_date,
                    visi_op = vISITA_ASSIGN.visi_op,
                    visi_status = vISITA_ASSIGN.visi_status,
                    visi_hora = vISITA_ASSIGN.visi_hora,
                    visi_km = vISITA_ASSIGN.visi_km
                };

                db.VISITA_ASSIGN.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { vISITA_ASSIGN }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VISITA_ASSIGN_Destroy([DataSourceRequest]DataSourceRequest request, VISITA_ASSIGN vISITA_ASSIGN)
        {
            if (ModelState.IsValid)
            {
                var entity = new VISITA_ASSIGN
                {
                    inst_id = vISITA_ASSIGN.inst_id,
                    cust_id = vISITA_ASSIGN.cust_id,
                    visi_id = vISITA_ASSIGN.visi_id,
                    visi_date = vISITA_ASSIGN.visi_date,
                    visi_op = vISITA_ASSIGN.visi_op,
                    visi_status = vISITA_ASSIGN.visi_status,
                    visi_hora = vISITA_ASSIGN.visi_hora,
                    visi_km = vISITA_ASSIGN.visi_km
                };

                db.VISITA_ASSIGN.Attach(entity);
                db.VISITA_ASSIGN.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { vISITA_ASSIGN }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SetVisita(int Inst, int Cust, int Prio, string Fecha)
        {
            try
            {
                int user =Convert.ToInt32(Session["Usua_id"].ToString());
                char[] delimiterChars = { ' ', '\t' };
                string[] words = Fecha.Split(delimiterChars);
                var fecha = words[0];
                var hora = words[1];
                var km = db.CUSTOMER.Where(a => a.cust_id == Cust).ToList();
                var k = Convert.ToInt32(km[0].cust_km);
                string ok="";
                var buscar = db.VISITA_ASSIGN.Where(a => a.inst_id == Inst && a.visi_hora == hora);
                if (buscar.Count() == 0) { 

                var entity = new VISITA_ASSIGN
                {
                    inst_id = Inst,
                    cust_id = Cust,
                    visi_op = Prio,
                    visi_date = fecha,
                    visi_status = 0,
                    visi_hora = hora,
                    visi_km=k,
                    user_id=user
                };
                db.VISITA_ASSIGN.Add(entity);
                db.SaveChanges();
                    return Json(new { success = true, responseText = "Cita Asignada con Exito.!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "El instructor seleccionado ya tiene cita programada a las "+hora+"." }, JsonRequestBehavior.AllowGet);
                }

               // return Json(new { succes = ok });
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = "Error!" }, JsonRequestBehavior.AllowGet);
            }


        }

    }
}
