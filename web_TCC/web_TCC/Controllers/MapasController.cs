using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_TCC.Models;


namespace web_TCC.Controllers
{
    public class MapasController : Controller
    {
        private web_TCCContext db = new web_TCCContext();
        // GET: Mapas
        public ActionResult Index()
        {
            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
            ViewBag.lat = null;
            ViewBag.lng = null;
            return View();

            //DateTime dataEscolhida = Convert.ToDateTime("27/08/2016");

            //var latitude =
            //        (from r in db.Registros
            //         where EntityFunctions.TruncateTime(r.Data) >= dataEscolhida
            //         && EntityFunctions.TruncateTime(r.Data) <= dataEscolhida
            //         && r.Entrada == true
            //         && r.ID_linha == 11
            //         orderby r.Latitude
            //         select r.Latitude).ToList();

            //var longitude =
            //        (from r in db.Registros
            //         where EntityFunctions.TruncateTime(r.Data) >= dataEscolhida
            //         && EntityFunctions.TruncateTime(r.Data) <= dataEscolhida
            //         && r.Entrada == true
            //         && r.ID_linha == 11
            //         orderby r.Longitude
            //         select r.Longitude).ToList();

            //ViewBag.lat = latitude.ToList();
            //ViewBag.lng = longitude.ToList();

            //return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                long ID_linha = form["ID_linha"] == "" ? 0 : long.Parse(form["ID_linha"]);
                string dataInicio = form["txtGetDataInicio"] == "" ? "" : form["txtGetDataInicio"];
                string dataFim = form["txtGetDataFim"] == "" ? dataInicio : form["txtGetDataFim"];

                if (dataInicio != "" && ID_linha != 0)
                {
                    DateTime dataEscolhidaInicio = Convert.ToDateTime(dataInicio);
                    DateTime dataEscolhidaFim = Convert.ToDateTime(dataFim);

                    var latitude =
                     (from r in db.Registros
                      where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                      && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                      && r.ID_linha == ID_linha
                      orderby r.Latitude
                      select r.Latitude).ToList();

                    var longitude =
                            (from r in db.Registros
                             where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                             && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                             && r.ID_linha == ID_linha
                             orderby r.Longitude
                             select r.Longitude).ToList();

                    ViewBag.lat = latitude.ToList();
                    ViewBag.lng = longitude.ToList();

                    if (!latitude.Any())
                    {
                        ModelState.AddModelError(String.Empty, "Nenhum registro com a Data e Linha informada");
                    }

                    ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");

                    return View();
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Informe a data e a linha.");
                    ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
                    ViewBag.lat = null;
                    ViewBag.lng = null;
                    return Index();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, "Erro.");
                ViewBag.lat = null;
                ViewBag.lng = null;
                return Index();
            }
        }
    }
}