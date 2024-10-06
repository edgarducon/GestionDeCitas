using CitasMicorservice.Models;
using CitasMicroservice.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace CitasMicroservice.Controllers
{
    [RoutePrefix("citas")]
    public class CitasController : ApiController
    {
        private CitasDbContext db = new CitasDbContext();

        // GET: citas
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCitas()
        {
            var citas = db.Citas.ToList();
            return Ok(citas);
        }

        // GET: citas/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCita(int id)
        {
            var cita = db.Citas.Find(id);
            if (cita == null)
            {
                return NotFound();
            }
            return Ok(cita);
        }

        // POST: citas
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateCita([FromBody] Citas cita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Citas.Add(cita);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + cita.Id), cita);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateCita(int id, [FromBody] Citas cita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCita = db.Citas.Find(id);
            if (existingCita == null)
            {
                return NotFound();
            }

            // Actualizar los campos
            existingCita.FechaCita = cita.FechaCita;
            existingCita.Lugar = cita.Lugar;
            existingCita.EstadoCitaId = cita.EstadoCitaId;
            existingCita.MedicoId = cita.MedicoId;
            existingCita.PacienteId = cita.PacienteId;

            db.Entry(existingCita).State = EntityState.Modified;
            db.SaveChanges();

            if (existingCita.EstadoCitaId == 3)
            {
                SendMessageToRabbitMQ(existingCita);
            }

            return Ok(existingCita);
        }

        private void SendMessageToRabbitMQ(Citas cita)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "recetas_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string fechaCitaFormatted = cita.FechaCita.ToString("yyyy-MM-dd");

                string message = $"{{ \"CitaId\": {cita.Id}, \"PacienteId\": {cita.PacienteId}, \"MedicoId\": {cita.MedicoId}, \"FechaCita\": \"{fechaCitaFormatted}\", \"Descripcion\": \"Receta generada tras cita enviada\" }}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "recetas_queue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"[x] Mensaje enviado: {message}");
            }
        }


        // DELETE: citas/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteCita(int id)
        {
            var cita = db.Citas.Find(id);
            if (cita == null)
            {
                return NotFound();
            }

            db.Citas.Remove(cita);
            db.SaveChanges();

            return Ok(cita);
        }
    }
}

