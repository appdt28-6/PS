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
    public class UsuariosController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Usuarios()
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

        public ActionResult USUARIO_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<USUARIO> usuario = db.USUARIO;
            DataSourceResult result = usuario.ToDataSourceResult(request, uSUARIO => new {
                Usua_Id = uSUARIO.Usua_Id,
                Usua_nombre = uSUARIO.Usua_nombre,
                Usua_Telefono = uSUARIO.Usua_Telefono,
                Usua_Email = uSUARIO.Usua_Email,
                Usua_Login = uSUARIO.Usua_Login,
                Usua_Password = uSUARIO.Usua_Password,
                Usua_Activo = uSUARIO.Usua_Activo,
                Usua_FechaCreacion = uSUARIO.Usua_FechaCreacion,
                Usua_Role = uSUARIO.Usua_Role
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIO_Create([DataSourceRequest]DataSourceRequest request, USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd");
                var entity = new USUARIO
                {
                    Usua_Id = uSUARIO.Usua_Id,
                    Usua_nombre = uSUARIO.Usua_nombre,
                    Usua_Telefono = uSUARIO.Usua_Telefono,
                    Usua_Email = uSUARIO.Usua_Email,
                    Usua_Login = uSUARIO.Usua_Login,
                    Usua_Password = uSUARIO.Usua_Password,
                    Usua_Activo = uSUARIO.Usua_Activo,
                    Usua_FechaCreacion = fecha,
                    Usua_Role = uSUARIO.Usua_Role
                };

                db.USUARIO.Add(entity);
                db.SaveChanges();
                uSUARIO.Usua_Id = entity.Usua_Id;
            }

            return Json(new[] { uSUARIO }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIO_Update([DataSourceRequest]DataSourceRequest request, USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                var entity = new USUARIO
                {
                    Usua_Id = uSUARIO.Usua_Id,
                    Usua_nombre = uSUARIO.Usua_nombre,
                    Usua_Telefono = uSUARIO.Usua_Telefono,
                    Usua_Email = uSUARIO.Usua_Email,
                    Usua_Login = uSUARIO.Usua_Login,
                    Usua_Password = uSUARIO.Usua_Password,
                    Usua_Activo = uSUARIO.Usua_Activo,
                    Usua_FechaCreacion = uSUARIO.Usua_FechaCreacion,
                    Usua_Role = uSUARIO.Usua_Role
                };

                db.USUARIO.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { uSUARIO }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult USUARIO_Destroy([DataSourceRequest]DataSourceRequest request, USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                var entity = new USUARIO
                {
                    Usua_Id = uSUARIO.Usua_Id,
                    Usua_nombre = uSUARIO.Usua_nombre,
                    Usua_Telefono = uSUARIO.Usua_Telefono,
                    Usua_Email = uSUARIO.Usua_Email,
                    Usua_Login = uSUARIO.Usua_Login,
                    Usua_Password = uSUARIO.Usua_Password,
                    Usua_Activo = uSUARIO.Usua_Activo,
                    Usua_FechaCreacion = uSUARIO.Usua_FechaCreacion,
                    Usua_Role = uSUARIO.Usua_Role
                };

                db.USUARIO.Attach(entity);
                db.USUARIO.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { uSUARIO }.ToDataSourceResult(request, ModelState));
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
