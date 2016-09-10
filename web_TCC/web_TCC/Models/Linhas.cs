namespace web_TCC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Linhas
    {
        [Key]
        public int ID_linha { get; set; }

        [Required]
        [StringLength(10)]
        public string Numero { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Registros> Registros { get; set; }
    }
}
