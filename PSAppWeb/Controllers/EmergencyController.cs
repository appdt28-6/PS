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
        AppDTEntities db = new AppDTEntities();
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
            var buscar = db.VISITA_REGISTRO.Where(a=>a.inst_id== inst_id && a.reg_status==8).ToList();
            if (buscar.Count() == 0)
            {
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

                        succes = "Ok";
                    }

                    catch (Exception e)
                    {
                        succes = "NoOk";
                    }
                }
            }
            else
            {

                if (buscar[0].reg_emo == 1)
                {
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

                            succes = "Ok";
                        }

                        catch (Exception e)
                        {
                            succes = "NoOk";
                        }
                    }
                }
                else
                {
                    if (buscar[0].inst_id != 0)
                    {
                        int id = buscar[0].inst_id;
                        var result2 = from r in db.VISITA_REGISTRO where r.inst_id == id && r.reg_status == 8 select r;
                        VISITA_REGISTRO vISITA_ASSIGN = result2.First();
                        vISITA_ASSIGN.reg_end = hr;
                        vISITA_ASSIGN.reg_emo = 1;
                        db.SaveChanges();
                        succes = "Ok";
                    }
                    else
                    {
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

                                succes = "Ok";

                            }
                            catch (Exception e)
                            {
                                succes = "NoOk";
                            }

                        }

                    }
                }

            }



            return Json(new { succes }, JsonRequestBehavior.AllowGet);
        }
    }
}