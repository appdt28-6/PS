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
    public class InstructorController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Instructor()
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

        public ActionResult INSTRUCTOR_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<INSTRUCTOR> instructor = db.INSTRUCTOR;
            DataSourceResult result = instructor.ToDataSourceResult(request, iNSTRUCTOR => new {
                inst_id = iNSTRUCTOR.inst_id,
                inst_name = iNSTRUCTOR.inst_name,
                inst_phone = iNSTRUCTOR.inst_phone,
                inst_mail = iNSTRUCTOR.inst_mail,
                user_id = iNSTRUCTOR.user_id,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult INSTRUCTOR_Create([DataSourceRequest]DataSourceRequest request, INSTRUCTOR iNSTRUCTOR)
        {
            if (ModelState.IsValid)
            {
                int user = Convert.ToInt32(Session["user_id"].ToString());
                var entity = new INSTRUCTOR
                {
                    inst_id = iNSTRUCTOR.inst_id,
                    inst_name = iNSTRUCTOR.inst_name,
                    inst_phone = iNSTRUCTOR.inst_phone,
                    inst_mail = iNSTRUCTOR.inst_mail,
                    user_id = user,
                };

                db.INSTRUCTOR.Add(entity);
                db.SaveChanges();
                iNSTRUCTOR.inst_id = entity.inst_id;
            }

            return Json(new[] { iNSTRUCTOR }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult INSTRUCTOR_Update([DataSourceRequest]DataSourceRequest request, INSTRUCTOR iNSTRUCTOR)
        {
            if (ModelState.IsValid)
            {
                int user = Convert.ToInt32(Session["user_id"].ToString());
                var entity = new INSTRUCTOR
                {
                    inst_id = iNSTRUCTOR.inst_id,
                    inst_name = iNSTRUCTOR.inst_name,
                    inst_phone = iNSTRUCTOR.inst_phone,
                    inst_mail = iNSTRUCTOR.inst_mail,
                    user_id = user,
                };

                db.INSTRUCTOR.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { iNSTRUCTOR }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult INSTRUCTOR_Destroy([DataSourceRequest]DataSourceRequest request, INSTRUCTOR iNSTRUCTOR)
        {
            if (ModelState.IsValid)
            {
                var entity = new INSTRUCTOR
                {
                    inst_id = iNSTRUCTOR.inst_id,
                    inst_name = iNSTRUCTOR.inst_name,
                    inst_phone = iNSTRUCTOR.inst_phone,
                    inst_mail = iNSTRUCTOR.inst_mail,
                    user_id = iNSTRUCTOR.user_id,
                };

                db.INSTRUCTOR.Attach(entity);
                db.INSTRUCTOR.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { iNSTRUCTOR }.ToDataSourceResult(request, ModelState));
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
    }
}
