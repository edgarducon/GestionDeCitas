using CitasMicorservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CitasMicroservice.Models
{
    public class CitasDbContext : DbContext
    {
        public CitasDbContext() : base("name=CitasDbContext")
        {
        }

        public DbSet<Citas> Citas { get; set; }
        public DbSet<EstadoCita> EstadoCitas { get; set; }
    }
}