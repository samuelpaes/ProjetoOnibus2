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

namespace LiveBus.Controllers
{
    [Authorize]
    public class LinhasController : Controller
    {
        private web_TCCContext db = new web_TCCContext();

        // GET: Linhas
        public ActionResult Index()
        {

            return View(db.Linhas.ToList());
        }

        // GET: Linhas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linhas Linhas = db.Linhas.Find(id);
            if (Linhas == null)
            {
                return HttpNotFound();
            }
            return View(Linhas);
        }

        // GET: Linhas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Linhas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_linha,Numero,Descricao,Ativo")] Linhas Linhas)
        {
            if (ModelState.IsValid)
            {
                db.Linhas.Add(Linhas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Linhas);
        }

        // GET: Linhas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linhas Linhas = db.Linhas.Find(id);
            if (Linhas == null)
            {
                return HttpNotFound();
            }
            return View(Linhas);
        }

        // POST: Linhas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_linha,Numero,Descricao,Ativo")] Linhas Linhas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Linhas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Linhas);
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
