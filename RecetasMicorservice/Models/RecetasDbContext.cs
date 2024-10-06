using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecetasMicorservice.Models
{
    public class RecetasDbContext : DbContext
    {
        public RecetasDbContext() : base("name=RecetasDbContext")
        {
        }
        public DbSet<Recetas> Recetas { get; set; }
        public DbSet<EstadoReceta> EstadoRecetas { get; set; }
    }

}