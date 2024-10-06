using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasMicorservice.Models
{
    public class RecetaMessage
    {
        public int CitaId { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCita { get; set; }
    }
}
