using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using web_TCC.Models;

namespace web_TCC.Controllers
{
    public class RelatoriosController : Controller
    {
        private web_TCCContext db = new web_TCCContext();


        // GET: Relatorios
        public ActionResult Index()
        {
            return View();
        }

        // GET: Relatorios/Geral
        public ActionResult Geral()
        {
            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
            return View();
        }

        // GET: Relatorios/Geral
        [HttpPost]
        public ActionResult Geral (FormCollection form)
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

                    var qRelatorio =
                    from r in db.Registros
                    join p in db.Pontos on r.ID_ponto equals p.ID_ponto
                    join l in db.Linhas on r.ID_linha equals l.ID_linha
                    where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                    && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                    && r.ID_linha == ID_linha
                    orderby r.NumeroVeiculo, r.Data
                    select new V_RegistrosLinhasPontos { Registros = r, Pontos = p, Linhas = l };

                    if (!qRelatorio.Any())
                    {
                        ModelState.AddModelError(String.Empty, "Nenhum registro com a Data e Linha informada");
                    }

                    ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
                    return View(qRelatorio.ToList());
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Informe a data e a linha.");
                    return Geral();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, "Erro.");
                return Geral();
            }
        }

        // GET: Relatorios/Pontos
        public ActionResult Pontos()
        {
            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
            return View();
        }

        // GET: Relatorios/Pontos
        [HttpPost]
        public ActionResult Pontos(FormCollection form)
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
                    
                    var qRelatorio = from r in db.Registros
                    where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                    && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                        orderby r.Linhas.ID_linha, r.NumeroVeiculo, r.Data
                        group r by new
                        {
                            r.Linhas.ID_linha,
                            r.Linhas.Numero,
                            r.NumeroVeiculo,
                            r.Data,
                            r.Pontos.ID_ponto,
                            r.Pontos.Descricao,
                            r.Pontos.Código,
                        } into g
                        select new V_RelRegistroPontos
                        {
                            LinhaId = g.Key.ID_linha,
                            LinhaNumero = g.Key.Numero,
                            RelatorioData = g.Key.Data,
                            PontoId = g.Key.ID_ponto,
                            PontoCodigo = g.Key.Código,
                            PontoDescricao = g.Key.Descricao,
                            RelatorioEntradaQuantidade = g.Count(x => x.Entrada == true),
                            RelatorioSaidaQuantidade = g.Count(x => x.Entrada == false),
                            RelatorioNumeroVeiculo = g.Key.NumeroVeiculo
                        };

                    if (!qRelatorio.Any())
                    {
                        ModelState.AddModelError(String.Empty, "Nenhum registro com a Data e Linha informada");
                    }

                    ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
                    return View(qRelatorio.ToList());
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Escolha uma data e uma linha de ônibus");
                    return Pontos();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, "Erro");
                return Pontos();
            }
        }

        // GET: Relatorios/Total
        public ActionResult Total()
        {
            ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
            return View();
        }

        // GET: Relatorios/Total
        [HttpPost]
        public ActionResult Total(FormCollection form)
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

                    var qRelatorio = from r in db.Registros
                        where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                        && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                        orderby r.ID_linha, r.Data
                        group r by new
                        {
                            r.Data.Year,
                            r.Data.Month,
                            r.Data.Day,
                            r.Linhas.Numero
                        } into g
                        select new V_RelRegistroTotal
                        {
                            //LinhaId = g.Key.ID_linha,
                            LinhaNumero = g.Key.Numero,
                            RegistroTotalPessoas = g.Count(x => x.Entrada == true),
                            Ano = g.Key.Year.ToString(),
                            Mes = g.Key.Month.ToString(),
                            Dia = g.Key.Day.ToString()
                        };

                    if (!qRelatorio.Any())
                    {
                        ModelState.AddModelError(String.Empty, "Nenhum registro com a Data e Linha informada");
                    }

                    ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
                    return View(qRelatorio.ToList());
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Escolha uma data e uma linha de ônibus");
                    return Pontos();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, "Erro");
                return Pontos();
            }
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