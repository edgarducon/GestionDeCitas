using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PersonasMicroservice.Models
{
    public class PersonasDbContext : DbContext
    {
        public PersonasDbContext() : base("name=PersonasDbContext")
        {
        }
        public DbSet<Personas> Personas { get; set; }
        public DbSet<TipoPersona> TipoPersonas { get; set; }
    }
}