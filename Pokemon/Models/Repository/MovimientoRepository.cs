using Dapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;

namespace Pokemon.Models.Repository
{
    public class MovimientoRepository :IMovimientoRepository
    {
        private readonly Conexion _conexion;

        public MovimientoRepository(Conexion conexion)
        {
            _conexion = conexion;
        }
        public async Task<IEnumerable<Movimiento>> GetMovimientos(int id)
        {
            var query = $"SELECT STUFF((SELECT ', ' + m.nombre FROM movimiento" +
                $" m JOIN pokemon_movimiento_forma pmf ON pmf.id_movimiento = " +
                $"m.id_movimiento JOIN pokemon p ON pmf.numero_pokedex = " +
                $"p.numero_pokedex WHERE p.numero_pokedex = {id} FOR XML PATH(''))," +
                $" 1, 2, '') AS nombre";
            using (var connection = _conexion.ObtenerConexion())
            {
                var movimiento = await connection.QueryAsync<Movimiento>(query);
                return movimiento.ToList();
            }
        }
    }
}
