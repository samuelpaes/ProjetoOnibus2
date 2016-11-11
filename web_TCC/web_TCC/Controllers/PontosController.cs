using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web_TCC.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.IO;

namespace web_TCC.Controllers
{
    [Authorize]
    public class PontosController : Controller
    {
        private web_TCCContext db = new web_TCCContext();

        // GET: Pontos
        public ActionResult Index()
        {
            return View(db.Pontos.ToList());
        }

        // GET: Pontos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pontos pontos = db.Pontos.Find(id);
            if (pontos == null)
            {
                return HttpNotFound();
            }
            return View(pontos);
        }

        // GET: Pontos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pontos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ponto,Código,Descricao,Latitude,Longitude,Ativo")] Pontos pontos)
        {
            if (ModelState.IsValid)
            {
                db.Pontos.Add(pontos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pontos);
        }

        // GET: Pontos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pontos pontos = db.Pontos.Find(id);
            if (pontos == null)
            {
                return HttpNotFound();
            }
            return View(pontos);
        }

        // POST: Pontos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ponto,Código,Descricao,Latitude,Longitude,Ativo")] Pontos pontos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pontos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pontos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

