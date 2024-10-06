using RecetasMicorservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RecetasMicorservice.Interfaces
{
    public interface IRecetasRepository
    {
        Task<List<Recetas>> GetRecetas();
        Task<Recetas> GetReceta(int id);
        Task<Recetas> CreateReceta(Recetas receta);
        Task<Recetas> UpdateReceta(int id, [FromBody] Recetas receta);
        Task<bool> DeleteReceta(int id);
        bool CreateRecetaFromMessage(Recetas nuevaReceta);
    }
}
