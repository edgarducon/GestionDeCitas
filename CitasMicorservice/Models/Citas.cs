using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitasMicorservice.Models
{
    public class Citas
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaCita { get; set; }
        public string Lugar { get; set; }   
        public int EstadoCitaId { get; set;}
        public int MedicoId { get; set; }
        public int PacienteId { get;set; }
    }
}