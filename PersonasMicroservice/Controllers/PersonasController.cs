using PersonasMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonasMicroservice.Controllers
{
    [RoutePrefix("personas")]
    public class PersonasController : ApiController
    {
        private PersonasDbContext db = new PersonasDbContext();

        // GET: personas
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetPersonas()
        {
            var personas = db.Personas.ToList();
            return Ok(personas);
        }

        // GET: personas/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetPersona(int id)
        {
            var persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        // POST: personas
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreatePersona([FromBody] Personas persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personas.Add(persona);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + persona.Id), persona);
        }

        // PUT: personas/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdatePersona(int id, [FromBody] Personas persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPersona = db.Personas.Find(id);
            if (existingPersona == null)
            {
                return NotFound();
            }

            // Actualizar los campos
            existingPersona.Nombre = persona.Nombre;
            existingPersona.Apellido = persona.Apellido;
            existingPersona.FechaNacimiento = persona.FechaNacimiento;
            existingPersona.TipoPersonaId = persona.TipoPersonaId;

            db.Entry(existingPersona).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(existingPersona);
        }

        // DELETE: personas/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeletePersona(int id)
        {
            var persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.Personas.Remove(persona);
            db.SaveChanges();

            return Ok(persona);
        }
    }
}
