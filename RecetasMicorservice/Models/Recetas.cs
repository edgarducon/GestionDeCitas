using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecetasMicorservice.Models
{
    public class Recetas
    {
        [Key]
        public int Id { get; set; }
        public string CodigoUnico { get; set; }
        public DateTime FechaEmision { get; set; }
        public int EstadoRecetaId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int CitaId { get; set; }
        public string Descripcion { get; set; }
    }
}