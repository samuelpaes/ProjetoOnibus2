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
    [Authorize]
    public class RelatoriosController : Controller
    {
        private web_TCCContext db = new web_TCCContext();


        // GET: Relatorios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult _GraficoGeral()
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
        public ActionResult Geral(FormCollection form)
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

                    var qRelatorioGraficoEmb =
                    from r in db.Registros
                    join p in db.Pontos on r.ID_ponto equals p.ID_ponto
                    join l in db.Linhas on r.ID_linha equals l.ID_linha
                    where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                    && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                    && r.ID_linha == ID_linha
                    orderby r.NumeroVeiculo, r.Data
                    select new V_RegistrosGraficoTotal
                    {
                        Hora = r.Data.Hour.ToString(),
                        Passageiros = r.Entrada.ToString()
                    };

                    int hora0 = 0, hora1 = 0, hora2 = 0, hora3 = 0, hora4 = 0, hora5 = 0, hora6 = 0, hora7 = 0, hora8 = 0, hora9 = 0, hora10 = 0, hora11 = 0;
                    int hora12 = 0, hora13 = 0, hora14 = 0, hora15 = 0, hora16 = 0, hora17 = 0, hora18 = 0, hora19 = 0, hora20 = 0, hora21 = 0, hora22 = 0, hora23 = 0;

                    foreach (var element in qRelatorioGraficoEmb)
                    {
                        if (element.Hora.ToString().Substring(0, 2) == "0")
                        {
                            hora0++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "1")
                        {
                            hora1++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "2")
                        {
                            hora2++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "3")
                        {
                            hora3++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "4")
                        {
                            hora4++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "5")
                        {
                            hora5++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "6")
                        {
                            hora6++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "7")
                        {
                            hora7++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "8")
                        {
                            hora8++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "9")
                        {
                            hora9++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "10")
                        {
                            hora10++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "11")
                        {
                            hora11++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "12")
                        {
                            hora12++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "13")
                        {
                            hora13++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "14")
                        {
                            hora14++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "15")
                        {
                            hora15++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "16")
                        {
                            hora16++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "17")
                        {
                            hora17++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "18")
                        {
                            hora18++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "19")
                        {
                            hora19++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "20")
                        {
                            hora20++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "21")
                        {
                            hora21++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "22")
                        {
                            hora22++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "23")
                        {
                            hora23++;
                        }
                    }

                    ViewBag.emb0 = hora0;
                    ViewBag.emb1 = hora1;
                    ViewBag.emb2 = hora2;
                    ViewBag.emb3 = hora3;
                    ViewBag.emb4 = hora4;
                    ViewBag.emb5 = hora5;
                    ViewBag.emb6 = hora6;
                    ViewBag.emb7 = hora7;
                    ViewBag.emb8 = hora8;
                    ViewBag.emb9 = hora9;
                    ViewBag.emb10 = hora10;
                    ViewBag.emb11 = hora11;
                    ViewBag.emb12 = hora12;
                    ViewBag.emb13 = hora13;
                    ViewBag.emb14 = hora14;
                    ViewBag.emb15 = hora15;
                    ViewBag.emb16 = hora16;
                    ViewBag.emb17 = hora17;
                    ViewBag.emb18 = hora18;
                    ViewBag.emb19 = hora19;
                    ViewBag.emb20 = hora20;
                    ViewBag.emb21 = hora21;
                    ViewBag.emb22 = hora22;
                    ViewBag.emb23 =  hora23;

                    var qRelatorioGraficoDes =
                    from r in db.Registros
                    join p in db.Pontos on r.ID_ponto equals p.ID_ponto
                    join l in db.Linhas on r.ID_linha equals l.ID_linha
                    where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                    && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                    && r.ID_linha == ID_linha
                    && r.Entrada == false
                    orderby r.NumeroVeiculo, r.Data
                    select new V_RegistrosGraficoTotal
                    {
                        Hora = r.Data.Hour.ToString(),
                        Passageiros = r.Entrada.ToString()
                    };

                    hora0 = 0;
                    hora1 = 0;
                    hora2 = 0;
                    hora3 = 0;
                    hora4 = 0;
                    hora5 = 0;
                    hora6 = 0;
                    hora7 = 0;
                    hora8 = 0;
                    hora9 = 0;
                    hora10 = 0;
                    hora11 = 0;
                    hora12 = 0;
                    hora13 = 0;
                    hora14 = 0;
                    hora15 = 0;
                    hora16 = 0;
                    hora17 = 0;
                    hora18 = 0;
                    hora19 = 0;
                    hora20 = 0;
                    hora21 = 0;
                    hora22 = 0;
                    hora23 = 0;

                    foreach (var element in qRelatorioGraficoDes)
                    {
                        if (element.Hora.ToString().Substring(0, 2) == "0")
                        {
                            hora0++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "1")
                        {
                            hora1++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "2")
                        {
                            hora2++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "3")
                        {
                            hora3++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "4")
                        {
                            hora4++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "5")
                        {
                            hora5++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "6")
                        {
                            hora6++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "7")
                        {
                            hora7++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "8")
                        {
                            hora8++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "9")
                        {
                            hora9++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "10")
                        {
                            hora10++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "11")
                        {
                            hora11++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "12")
                        {
                            hora12++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "13")
                        {
                            hora13++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "14")
                        {
                            hora14++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "15")
                        {
                            hora15++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "16")
                        {
                            hora16++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "17")
                        {
                            hora17++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "18")
                        {
                            hora18++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "19")
                        {
                            hora19++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "20")
                        {
                            hora20++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "21")
                        {
                            hora21++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "22")
                        {
                            hora22++;
                        }
                        else if (element.Hora.ToString().Substring(0, 2) == "23")
                        {
                            hora23++;
                        }
                    }

                    ViewBag.des0 = hora0;
                    ViewBag.des1 = hora1;
                    ViewBag.des2 = hora2;
                    ViewBag.des3 = hora3;
                    ViewBag.des4 = hora4;
                    ViewBag.des5 = hora5;
                    ViewBag.des6 = hora6;
                    ViewBag.des7 = hora7;
                    ViewBag.des8 = hora8;
                    ViewBag.des9 = hora9;
                    ViewBag.des10 = hora10;
                    ViewBag.des11 = hora11;
                    ViewBag.des12 = hora12;
                    ViewBag.des13 = hora13;
                    ViewBag.des14 = hora14;
                    ViewBag.des15 = hora15;
                    ViewBag.des16 = hora16;
                    ViewBag.des17 = hora17;
                    ViewBag.des18 = hora18;
                    ViewBag.des19 = hora19;
                    ViewBag.des20 = hora20;
                    ViewBag.des21 = hora21;
                    ViewBag.des22 = hora22;
                    ViewBag.des23 = hora23;
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
                                     && r.Linhas.ID_linha == ID_linha
                                     orderby r.Linhas.ID_linha, r.NumeroVeiculo, r.Data
                                     group r by new
                                     {
                                         r.Linhas.ID_linha,
                                         r.Linhas.Numero,
                                         r.NumeroVeiculo,
                                         r.Pontos.ID_ponto,
                                         r.Pontos.Descricao,
                                         r.Pontos.Código,
                                     } into g
                                     select new V_RelRegistroPontos
                                     {
                                         LinhaId = g.Key.ID_linha,
                                         LinhaNumero = g.Key.Numero,
                                         PontoId = g.Key.ID_ponto,
                                         PontoCodigo = g.Key.Código,
                                         PontoDescricao = g.Key.Descricao,
                                         RelatorioEntradaQuantidade = g.Count(x => x.Entrada == true),
                                         RelatorioSaidaQuantidade = g.Count(x => x.Entrada == false),
                                         RelatorioNumeroVeiculo = g.Key.NumeroVeiculo
                                     };


                    var qRelatorio2 = from r in db.Registros
                                      where EntityFunctions.TruncateTime(r.Data) >= dataEscolhidaInicio
                                      && EntityFunctions.TruncateTime(r.Data) <= dataEscolhidaFim
                                      orderby r.Linhas.ID_linha, r.NumeroVeiculo, r.Data
                                      group r by new
                                      {
                                          r.Linhas.Numero,
                                          r.Pontos.Código,
                                      } into g
                                      select new V_RelRegistroPontos
                                      {
                                          PontoCodigo = g.Key.Código,
                                          RelatorioEntradaQuantidade = g.Count(x => x.Entrada == true),
                                          RelatorioSaidaQuantidade = g.Count(x => x.Entrada == false),
                                      };

                    if (!qRelatorio.Any())
                    {
                        ModelState.AddModelError(String.Empty, "Nenhum registro com a Data e Linha informada");
                    }

                    ViewBag.RelPontos = qRelatorio2.ToList();

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
                                     && r.Linhas.ID_linha == ID_linha
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
                                         LinhaNumero = g.Key.Numero,
                                         RegistroTotalPessoas = g.Count(x => x.Entrada == true),
                                         Ano = g.Key.Year.ToString(),
                                         Mes = g.Key.Month.ToString(),
                                         Dia = g.Key.Day.ToString(),

                                         Data = (g.Key.Day.ToString() + "/" + g.Key.Month.ToString() + "/" + g.Key.Year.ToString()),
                                     };

                    var qRelatorio2 = from r in db.Registros
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
                                          RegistroTotalPessoas = g.Count(x => x.Entrada == true),
                                          Ano = g.Key.Year.ToString(),
                                          Mes = g.Key.Month.ToString(),
                                          Dia = g.Key.Day.ToString()
                                      };

                    if (!qRelatorio.Any())
                    {
                        ModelState.AddModelError(String.Empty, "Nenhum registro com a Data e Linha informada");
                    }

                    ViewBag.RelTotal = qRelatorio.ToList();

                    var myArrayDados = ViewBag.RelTotal;

                    ViewBag.ID_linha = new SelectList(db.Linhas, "ID_linha", "Numero");
                    return View(qRelatorio.ToList());
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Escolha um período e uma linha de ônibus");
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