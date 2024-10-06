using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitasMicorservice.Models
{
    public class EstadoCita
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set;}
    }
}