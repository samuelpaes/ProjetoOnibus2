using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web_TCC.Models
{
    public class Registros
    {
        [Key]
        public int ID_registro { get; set; }

        public bool Entrada { get; set; }

        public DateTime Data { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int QuantidadePessoas { get; set; }

        public string NumeroVeiculo { get; set; }

        public int ID_linha { get; set; }

        public int ID_ponto { get; set; }

        public virtual Linhas Linhas { get; set; }

        public virtual Pontos Pontos { get; set; }
    }
}