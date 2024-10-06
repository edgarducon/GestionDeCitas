using CitasMicorservice.Models;
using CitasMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CitasMicroservice.Controllers
{
    [RoutePrefix("estadocitas")]
    public class EstadoCitaController : ApiController
    {
        private CitasDbContext db = new CitasDbContext();

        // GET: estadocitas
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetEstadoCitas()
        {
            var estadoCitas = db.EstadoCitas.ToList();
            return Ok(estadoCitas);
        }

        // GET: estadocitas/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetEstadoCita(int id)
        {
            var estadoCita = db.EstadoCitas.Find(id);
            if (estadoCita == null)
            {
                return NotFound();
            }
            return Ok(estadoCita);
        }

        // POST: estadocitas
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateEstadoCita([FromBody] EstadoCita estadoCita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EstadoCitas.Add(estadoCita);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + estadoCita.Id), estadoCita);
        }

        // PUT: estadocitas/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateEstadoCita(int id, [FromBody] EstadoCita estadoCita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEstadoCita = db.EstadoCitas.Find(id);
            if (existingEstadoCita == null)
            {
                return NotFound();
            }

            // Actualizar los campos
            existingEstadoCita.Descripcion = estadoCita.Descripcion;

            db.Entry(existingEstadoCita).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(existingEstadoCita);
        }

        // DELETE: estadocitas/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteEstadoCita(int id)
        {
            var estadoCita = db.EstadoCitas.Find(id);
            if (estadoCita == null)
            {
                return NotFound();
            }

            db.EstadoCitas.Remove(estadoCita);
            db.SaveChanges();

            return Ok(estadoCita);
        }
    }
}
