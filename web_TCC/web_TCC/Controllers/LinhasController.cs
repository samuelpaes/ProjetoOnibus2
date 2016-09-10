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

namespace web_TCC.Controllers
{
    //[Authorize]
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Linhas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Linhas Linhas = db.Linhas.Find(id);
        //    if (Linhas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Linhas);
        //}

        //// POST: Linhas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Linhas Linhas = db.Linhas.Find(id);
        //    db.Linhas.Remove(Linhas);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
