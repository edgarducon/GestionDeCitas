using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonasMicroservice.Models;

namespace PersonasMicroservice.Controllers
{
    [RoutePrefix("api/tipopersonas")]
    public class TipoPersonasController : ApiController
    {
        private readonly PersonasDbContext _context;

        public TipoPersonasController()
        {
            _context = new PersonasDbContext();
        }

        // GET: api/tipopersonas
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetTipoPersonas()
        {
            try
            {
                var tipoPersonas = _context.TipoPersonas.ToList();
                return Ok(tipoPersonas);
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        // GET: api/tipopersonas/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetTipoPersona(int id)
        {
            var tipoPersona = _context.TipoPersonas.Find(id);
            if (tipoPersona == null)
            {
                return NotFound();
            }
            return Ok(tipoPersona);
        }

        // POST: api/tipopersonas
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostTipoPersona([FromBody] TipoPersona tipoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TipoPersonas.Add(tipoPersona);
            _context.SaveChanges();

            return Ok(tipoPersona);
        }

        // PUT: api/tipopersonas/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutTipoPersona(int id, [FromBody] TipoPersona tipoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoPersona.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoPersona).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoPersonaExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/tipopersonas/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteTipoPersona(int id)
        {
            var tipoPersona = _context.TipoPersonas.Find(id);
            if (tipoPersona == null)
            {
                return NotFound();
            }

            _context.TipoPersonas.Remove(tipoPersona);
            _context.SaveChanges();

            return Ok(tipoPersona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoPersonaExists(int id)
        {
            return _context.TipoPersonas.Count(e => e.Id == id) > 0;
        }
    }
}
