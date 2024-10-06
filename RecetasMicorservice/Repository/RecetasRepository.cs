using RecetasMicorservice.Interfaces;
using RecetasMicorservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RecetasMicorservice.Repository
{
    public class RecetasRepository : IRecetasRepository
    {
        private RecetasDbContext db = new RecetasDbContext();
        public Task<Recetas> CreateReceta(Recetas receta)
        {
            db.Recetas.Add(receta);
            db.SaveChanges();
            return Task.FromResult(receta);
        }

        public bool CreateRecetaFromMessage(Recetas nuevaReceta)
        {
            db.Recetas.Add(nuevaReceta);
            db.SaveChanges();
            return true;
        }

        public Task<bool> DeleteReceta(int id)
        {
            db.Recetas.Find(id);
            db.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<Recetas> GetReceta(int id)
        {
            Recetas receta = db.Recetas.Find(id);
            return Task.FromResult(receta);
        }

        public Task<List<Recetas>> GetRecetas()
        {
            List<Recetas> recetas = db.Recetas.ToList();
            return Task.FromResult(recetas);
        }

        public Task<Recetas> UpdateReceta(int id, [FromBody] Recetas receta)
        {
            var existingReceta = db.Recetas.Find(id);
            if (existingReceta == null)
                return null;

            existingReceta.CodigoUnico = receta.CodigoUnico;
            existingReceta.FechaEmision = receta.FechaEmision;
            existingReceta.EstadoRecetaId = receta.EstadoRecetaId;
            existingReceta.MedicoId = receta.MedicoId;
            existingReceta.PacienteId = receta.PacienteId;
            existingReceta.CitaId = receta.CitaId;
            existingReceta.Descripcion = receta.Descripcion;

            db.Entry(existingReceta).State = EntityState.Modified;
            db.SaveChanges();
            return Task.FromResult(existingReceta);
        }
    }
}