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
    //[Authorize]
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pontos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Pontos pontos = db.Pontos.Find(id);
        //    if (pontos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pontos);
        //}

        // POST: Pontos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Pontos pontos = db.Pontos.Find(id);
        //    db.Pontos.Remove(pontos);
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



        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase arquivoExterno)
        {
            try
            {
                string teste = "";
                bool ok = false;
                Pontos dados = new Pontos
                {
                    Código = "",
                    Descricao = "",
                    Latitude = "",
                    Longitude = "",
                    Ativo = true
                };

                if (arquivoExterno.ContentLength > 0)//verifica se o arquivo é maior que 0
                {
                    //Obtemm o Arquivo da View
                    var arquivoInterno = Path.GetFileName(arquivoExterno.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Temp"), arquivoInterno);
                    arquivoExterno.SaveAs(path);

                    //Captura o Arquivo salvo temporariamente para tratamento
                    var conteudoArquivo = System.IO.File.ReadAllText(path);

                    //Tratar arquivo
                    string[] array = System.IO.File.ReadAllLines(path);//Cria um array com uma linha para cada registro

                    for (int i = 0; i < array.Length; i++)
                    {

                        string[] auxiliar = array[i].Split(';');//Quebra a primeira linha e separa os dados
                        
                        if (auxiliar[0].Length > 4)
                        {

                            if (auxiliar[0].Substring(0, 2) == "-4")
                            {
                                //if (auxiliar[3] == "OR47")
                                //{
                                //    ok = true;
                                //}

                                //if (ok == true)
                                //{
                                    dados.Longitude = auxiliar[0];
                                    dados.Latitude = auxiliar[1];
                                    dados.Código = auxiliar[3];
                                    dados.Descricao = auxiliar[4];
                                    dados.Ativo = true;

                                    //Temos um problema aqui
                                    if (ModelState.IsValid)
                                    {
                                        db.Entry(dados).State = EntityState.Added;

                                        db.SaveChanges();
                                        ViewBag.Message = "Sucesso";
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Formato Inválido";
                                    }

                                }
                            }
                        }
                    }
                }
            //}

            catch (Exception e)
            {
                ViewBag.Message = e;

            }
                return View();
        }
    }
}

