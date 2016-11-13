using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_TCC.Models
{
    public class V_RelRegistroPontos
    {
        public long LinhaId { get; set; }

        public string LinhaNumero { get; set; }

        public long PontoId { get; set; }

        public string PontoCodigo { get; set; }

        public string PontoDescricao { get; set; }

        public DateTime RelatorioData { get; set; }

        public bool RelatorioEntrada { get; set; }

        public long RelatorioEntradaQuantidade { get; set; }

        public long RelatorioSaidaQuantidade { get; set; }

        public int RelatorioQuantidaPessoas { get; set; }

        public string RelatorioNumeroVeiculo { get; set; }
    }
}