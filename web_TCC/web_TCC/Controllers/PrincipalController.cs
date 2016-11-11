using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_TCC.Models;

namespace web_TCC.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        private web_TCCContext db = new web_TCCContext();
        private ApplicationUser db1 = new ApplicationUser();

        // GET: Principal
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            int totalUsuarios = context.Users.ToList().Count;
            TempData["Total de Usuários"] = context.Users.ToList().Count;

            int totalLinhas = (from p in db.Linhas select p).Count();
            TempData["Total de Linhas"] = totalLinhas;

            int totalPontos = (from p in db.Pontos select p).Count();
            TempData["Total de Pontos"] = totalPontos;

            int totalRegistros = (from p in db.Registros select p).Count();

            TempData["Total de Registros"] = totalRegistros;

            TempData["Total de Registros"] = totalRegistros;  

            return View();
        }
    }
}