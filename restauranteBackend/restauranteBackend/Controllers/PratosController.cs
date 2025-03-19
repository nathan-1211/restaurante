using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;  // Usando MySql.Data
using RestauranteBackend.Models;

namespace RestauranteBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PratosController : ControllerBase
    {
        private readonly string _connectionString;

        public PratosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("String de conexão não encontrada.");
        }

        [HttpGet]
        public IEnumerable<Prato> Get()
        {
            var pratos = new List<Prato>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Pratos", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pratos.Add(new Prato
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Ingredientes = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                    });
                }
            }
            return pratos;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Prato prato)
        {
            if (prato == null)
            {
                return BadRequest();
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO Pratos (Nome, Ingredientes) VALUES (@Nome, @Ingredientes); SELECT LAST_INSERT_ID();", connection);
                command.Parameters.AddWithValue("@Nome", prato.Nome);
                command.Parameters.AddWithValue("@Ingredientes", prato.Ingredientes);
                var id = command.ExecuteScalar();
                prato.Id = Convert.ToInt32(id);
            }

            return CreatedAtAction(nameof(Get), new { id = prato.Id }, prato);
        }
    }
}