using RecetasMicorservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using RecetasMicorservice.Interfaces;
using System.Threading.Tasks;

namespace RecetasMicroservice.Controllers
{
    [RoutePrefix("recetas")]
    public class RecetasController : ApiController
    {


        private readonly IRecetasRepository _recetasRepository;
        public RecetasController(IRecetasRepository RecetasRepository)
        {
            _recetasRepository = RecetasRepository;
            StartRabbitMQListener();

        }
        private void StartRabbitMQListener()
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[x] Mensaje recibido: {message}");

                    // Procesar el mensaje y crear la receta
                    CreateRecetaFromMessage(message);
                };

                channel.BasicConsume(queue: "recetas_queue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Escuchando mensajes en RabbitMQ...");
                Console.ReadLine(); // Mantener la conexión activa
            }
        }

        private void CreateRecetaFromMessage(string message)
        {
            try
            {
                var recetaData = JsonConvert.DeserializeObject<RecetaMessage>(message);

                var nuevaReceta = new Recetas
                {
                    CitaId = recetaData.CitaId,
                    PacienteId = recetaData.PacienteId,
                    MedicoId = recetaData.MedicoId,
                    FechaEmision = DateTime.Now,
                    Descripcion = recetaData.Descripcion,
                    EstadoRecetaId = 1,
                    CodigoUnico = Guid.NewGuid().ToString()
                };
                bool respuesta = _recetasRepository.CreateRecetaFromMessage(nuevaReceta);
                Console.WriteLine($"[x] Receta creada para la cita ID: {nuevaReceta.CitaId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear receta: {ex.Message}");
            }
        }

        // GET: recetas
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetRecetas()
        {
            var recetas = _recetasRepository.GetRecetas();
            return Ok(recetas);
        }

        // GET: recetas/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetReceta(int id)
        {
            var receta = _recetasRepository.GetReceta(id);
            if (receta == null)
            {
                return NotFound();
            }
            return Ok(receta);
        }

        // POST: recetas
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateReceta(Recetas receta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Recetas recetas = await _recetasRepository.CreateReceta(receta);

            return Created(new Uri(Request.RequestUri + "/" + receta.Id), receta);
        }

        // PUT: recetas/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateReceta(int id, [FromBody] Recetas receta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Recetas updatedReceta = await _recetasRepository.UpdateReceta(id, receta);

            if (updatedReceta == null)
                return NotFound();

            return Ok(updatedReceta);
        }

        // DELETE: recetas/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteReceta(int id)
        {
            var receta = _recetasRepository.DeleteReceta(id);
            if (receta == null)
                return NotFound();
            return Ok(receta);
        }
    }
}
