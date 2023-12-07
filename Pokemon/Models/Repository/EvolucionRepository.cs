using Dapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;

namespace Pokemon.Models.Repository
{
    public class EvolucionRepository : IEvolucionRepository
    {
        private readonly Conexion _conexion;

        public EvolucionRepository(Conexion conexion)
        {
            _conexion = conexion;
        }
        public async Task<Evolucion> GetEvolucion(int id)
        {
            var query = $"SELECT e.pokemon_evolucionado " +
                $"FROM pokemon AS p1 JOIN evoluciona_de AS e " +
                $"ON p1.numero_pokedex = e.pokemon_origen JOIN pokemon AS p2 " +
                $"ON e.pokemon_evolucionado = p2.numero_pokedex " +
                $"WHERE p1.numero_pokedex = { id}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var evolucion = await connection.QuerySingleOrDefaultAsync<Evolucion>(query);
                return evolucion;
            }
        }

        public async Task<Evolucion> GetOrigen(int id)
        {
            var query = $"SELECT e.pokemon_origen " +
                $"FROM pokemon AS p1 JOIN evoluciona_de AS e " +
                $"ON p1.numero_pokedex = e.pokemon_evolucionado JOIN pokemon AS p2 " +
                $"ON e.pokemon_evolucionado = p2.numero_pokedex " +
                $"WHERE p1.numero_pokedex = {id}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var origen = await connection.QuerySingleOrDefaultAsync<Evolucion>(query);
                return origen;
            }
        }
    }
}
