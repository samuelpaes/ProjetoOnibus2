using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_TCC.Models
{
    public class V_RelRegistroTotal
    {
        public long LinhaId { get; set; }

        public string LinhaNumero { get; set; }

        public string RegistroVeiculo { get; set; }

        public long RegistroTotalPessoas { get; set; }

        public string Dia { get; set; }

        public string Mes { get; set; }

        public string Ano { get; set; }

        public string Data { get; set; }
    }
}