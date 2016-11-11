using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace web_TCC.Models
{
    public class web_TCCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public web_TCCContext() : base("name=web_TCCContext")
        {
        }

        public System.Data.Entity.DbSet<web_TCC.Models.Linhas> Linhas { get; set; }

        public System.Data.Entity.DbSet<web_TCC.Models.Pontos> Pontos { get; set; }

        public System.Data.Entity.DbSet<web_TCC.Models.Registros> Registros { get; set;}
    
    }
}
