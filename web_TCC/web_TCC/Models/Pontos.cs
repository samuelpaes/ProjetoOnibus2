namespace web_TCC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pontos
    {
        [Key]
        public int ID_ponto { get; set; }

        [StringLength(50)]
        public string Código { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(50)]
        public string Latitude { get; set; }

        [Required]
        [StringLength(50)]
        public string Longitude { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Registros> Registros { get; set; }
    }
}
