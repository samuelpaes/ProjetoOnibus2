using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_TCC.Models
{
    public class V_RegistrosLinhasPontos
    {
        public Models.Registros Registros { get; set; }

        public Models.Pontos Pontos { get; set; }

        public Models.Linhas Linhas { get; set; }

        public string NumeroVeiculo { get; set; }
    }
}