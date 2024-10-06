using RecetasMicorservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecetasMicroservice.Controllers
{
    [RoutePrefix("estado-recetas")]
    public class EstadoRecetaController : ApiController
    {
        private RecetasDbContext db = new RecetasDbContext();

        // GET: estado-recetas
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetEstadosRecetas()
        {
            var estados = db.EstadoRecetas.ToList();
            return Ok(estados);
        }

        // GET: estado-recetas/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetEstadoReceta(int id)
        {
            var estado = db.EstadoRecetas.Find(id);
            if (estado == null)
            {
                return NotFound();
            }
            return Ok(estado);
        }

        // POST: estado-recetas
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateEstadoReceta([FromBody] EstadoReceta estadoReceta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EstadoRecetas.Add(estadoReceta);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + estadoReceta.Id), estadoReceta);
        }

        // PUT: estado-recetas/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateEstadoReceta(int id, [FromBody] EstadoReceta estadoReceta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEstado = db.EstadoRecetas.Find(id);
            if (existingEstado == null)
            {
                return NotFound();
            }

            // Actualizar los campos
            existingEstado.Descripcion = estadoReceta.Descripcion;

            db.Entry(existingEstado).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(existingEstado);
        }

        // DELETE: estado-recetas/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteEstadoReceta(int id)
        {
            var estado = db.EstadoRecetas.Find(id);
            if (estado == null)
            {
                return NotFound();
            }

            db.EstadoRecetas.Remove(estado);
            db.SaveChanges();

            return Ok(estado);
        }
    }
}
