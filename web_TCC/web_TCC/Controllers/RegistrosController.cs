using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web_TCC.Models;

namespace web_TCC.Controllers
{
    public class RegistrosController : Controller
    {
        private web_TCCContext db = new web_TCCContext();

        // GET: Registros
        public ActionResult Index()
        {
            var registros = db.Registros.Include(r => r.Linhas).Include(r => r.Pontos);
            return View(registros.ToList());
        }

        // GET: Registros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registros registros = db.Registros.Find(id);
            if (registros == null)
            {
                return HttpNotFound();
            }
            return View(registros);
        }

        // GET: Registros/Create
        public ActionResult Create()
        {
            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
            ViewBag.ID_ponto = new SelectList(db.Pontos, "ID_ponto", "Código");
            return View();
        }

        // POST: Registros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_registro,Entrada,Data,Latitude,Longitude,QuantidadePessoas,ID_linha,ID_ponto")] Registros registros)
        {
            if (ModelState.IsValid)
            {
                db.Registros.Add(registros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero", registros.ID_linha);
            ViewBag.ID_ponto = new SelectList(db.Pontos, "ID_ponto", "Código", registros.ID_ponto);
            return View(registros);
        }

        // GET: Registros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registros registros = db.Registros.Find(id);
            if (registros == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero", registros.ID_linha);
            ViewBag.ID_ponto = new SelectList(db.Pontos, "ID_ponto", "Código", registros.ID_ponto);
            return View(registros);
        }

        // POST: Registros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_registro,Entrada,Data,Latitude,Longitude,QuantidadePessoas,ID_linha,ID_ponto")] Registros registros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero", registros.ID_linha);
            ViewBag.ID_ponto = new SelectList(db.Pontos, "ID_ponto", "Código", registros.ID_ponto);
            return View(registros);
        }

        // GET: Registros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registros registros = db.Registros.Find(id);
            if (registros == null)
            {
                return HttpNotFound();
            }
            return View(registros);
        }

        // POST: Registros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registros registros = db.Registros.Find(id);
            db.Registros.Remove(registros);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Dados/Upload/6

        public ActionResult Upload()
        {
            return View();
        }


        // POST: Dados/Upload/6
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase arquivoExterno)
        {
            string teste = "";
           try
            {
                Registros dados = new Registros
                {
                    Entrada = false,
                    Data = DateTime.Today,
                    Latitude = "",
                    Longitude = "",
                    QuantidadePessoas = 0,
                    NumeroVeiculo = "",
                    ID_linha = 7,
                    ID_ponto = 2,
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
                        string[] auxiliar = array[i].Split('@');//Quebra a primeira linha e separa os dados

                        dados.Entrada = Convert.ToBoolean(auxiliar[0]);
                        dados.Data = Convert.ToDateTime(auxiliar[1]);
                        dados.Latitude = auxiliar[2];
                        dados.Longitude = auxiliar[3];
                        dados.QuantidadePessoas = Convert.ToInt32(auxiliar[4]);
                        dados.NumeroVeiculo = auxiliar[5];
                        /*dados.ID_linha = Convert.ToInt32(auxiliar[6]);
                        dados.ID_ponto = Convert.ToInt32(auxiliar[7]);*/

                        //busca na base o id da linha referente à linha informada.
                        dados.ID_linha = BuscarIdLinha(auxiliar[6]);
                        //calcula o ponto mais próximo das coordenadas capturadas
                        dados.ID_ponto = CalcularDistancia(auxiliar[2], auxiliar[3]); 

                        teste = auxiliar[0];

                       
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

            catch (Exception e)
            {
                ViewBag.Message = e;

            }

            return View();
        }

        private int BuscarIdLinha(string linha)
        {
            int resultado = (from r in db.Linhas
                           where r.Numero == linha
                           select r.ID_linha ).SingleOrDefault();

            return resultado;
        }

        private int CalcularDistancia(string lat, string lng)
        {
            Coordenadas coord = new Coordenadas();

            var qRelatorio =
               from n in db.Pontos
               where n.Ativo == true
               select new
               {
                   idPonto = n.ID_ponto,
                   latitude = n.Latitude,
                   longitude = n.Longitude
               };

            DataTable myDataTable = new DataTable();
            myDataTable.Columns.Add(
                new DataColumn()
                {
                    DataType = System.Type.GetType("System.String"),
                    ColumnName = "ID_PONTO"
                });
            myDataTable.Columns.Add(
                new DataColumn()
                {
                    DataType = System.Type.GetType("System.Int32"),
                    ColumnName = "DISTANCIA"
                });

            foreach (var element in qRelatorio)
            {
                var row = myDataTable.NewRow();
                row["ID_PONTO"] = element.idPonto;
                row["DISTANCIA"] = coord.getPontoPelaCoordenada(lat, lng, element.latitude, element.longitude);
                myDataTable.Rows.Add(row);

            }
            DataView view = new DataView(myDataTable);
            view.Sort = "DISTANCIA ASC";

            return Convert.ToInt32(view[0]["ID_PONTO"]);
        }



        public ActionResult InclusaoManualTeste()
        {
            return View();
        }
    }
}
    


    