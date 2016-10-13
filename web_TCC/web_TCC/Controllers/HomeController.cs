using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_TCC.Models;

namespace web_TCC.Controllers
{
    public class HomeController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
         private web_TCCContext db = new web_TCCContext();
        public ActionResult Index()
        {
            
            int quantidadeRegistros = (from p in db.Pontos select p).Count();
            ViewBag.Dados = quantidadeRegistros;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}