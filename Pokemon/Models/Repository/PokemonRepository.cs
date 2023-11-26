using Dapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Pokemon.Models;

namespace Pokemon.Models.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly Conexion _conexion;

        public PokemonRepository(Conexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Pokemon>> GetPokemons()
        {
            var query = "WITH RankedPokemon AS (SELECT p.*, tipo = STUFF((SELECT ', ' + tipo.nombre FROM tipo JOIN pokemon_tipo pt ON pt.id_tipo = tipo.id_tipo WHERE pt.numero_pokedex = p.numero_pokedex FOR XML PATH ('')), 1, 1, ''), evolucionado.nombre AS pokemon_evolucionado, origen.nombre AS pokemon_origen, ROW_NUMBER() OVER (PARTITION BY p.numero_pokedex ORDER BY (SELECT NULL)) AS RowNum FROM pokemon p FULL JOIN evoluciona_de e ON e.pokemon_origen = p.numero_pokedex LEFT JOIN pokemon evolucionado ON evolucionado.numero_pokedex = e.pokemon_evolucionado LEFT JOIN pokemon origen ON origen.numero_pokedex = e.pokemon_origen) SELECT * FROM RankedPokemon WHERE RowNum = 1;\r\n";
            using (var connection = _conexion.ObtenerConexion())
            {
                var pokemons = await connection.QueryAsync<Pokemon>(query);
                return pokemons.ToList();
            }
        }
        public async Task<Pokemon> GetPokemonById(int? id)
        {
            var query = $"SELECT TOP 1 p.*, tipo = STUFF((" +
                $"SELECT ', ' + tipo.nombre FROM tipo JOIN pokemon_tipo pt ON pt.id_tipo = tipo.id_tipo " +
                $"WHERE pt.numero_pokedex = p.numero_pokedex FOR XML PATH ('')), 1, 1, '')" +
                $", pokemon_evolucionado = STUFF((SELECT ', ' + evolucionado.nombre FROM pokemon p " +
                $"FULL JOIN evoluciona_de e ON e.pokemon_origen = p.numero_pokedex " +
                $"LEFT JOIN pokemon evolucionado ON evolucionado.numero_pokedex = e.pokemon_evolucionado " +
                $"LEFT JOIN pokemon origen ON origen.numero_pokedex = e.pokemon_origen Where p.numero_pokedex = {id} " +
                $"FOR XML PATH ('')), 1, 1, ''), origen.nombre AS pokemon_origen FROM pokemon p " +
                $"FULL JOIN evoluciona_de e ON e.pokemon_origen = p.numero_pokedex " +
                $"LEFT JOIN pokemon evolucionado ON evolucionado.numero_pokedex = e.pokemon_evolucionado " +
                $"LEFT JOIN pokemon origen ON origen.numero_pokedex = e.pokemon_origen Where p.numero_pokedex = {id}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var pokemon = await connection.QuerySingleOrDefaultAsync<Pokemon>(query, new { id });
                return pokemon;
            }
        }
        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetRandom(int count, string tipos, string filter, string direccion)
        {
            string tiposN = (tipos != null) ? tiposN = tipos.Split(" ")[0] : null;
            string filtroTipo = (tiposN != null) ? $"AND t.nombre IN ({tiposN})" : "";
            var query = $"SELECT rgp.* FROM (" +
                $"SELECT TOP {count} p.*, tipo = STUFF((SELECT ', ' + tipo.nombre " +
                $"FROM tipo tipo JOIN pokemon_tipo pt ON pt.id_tipo = tipo.id_tipo " +
                $"WHERE pt.numero_pokedex = p.numero_pokedex FOR XML PATH('')), 1, 1, '') " +
                $"FROM pokemon p WHERE EXISTS( SELECT 1 FROM pokemon_tipo pt JOIN tipo t ON pt.id_tipo = t.id_tipo " +
                $"WHERE pt.numero_pokedex = p.numero_pokedex {filtroTipo})ORDER BY NEWID())" +
                $" as rgp ORDER BY rgp.{filter} {direccion}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var randomPokemons = await connection.QueryAsync<Pokemon>(query);
                return randomPokemons.ToList();
            }
        }
        [HttpGet]
        public async Task<Movimiento> GetMovimiento(int id)
        {
            var query = $"SELECT STUFF((SELECT ', ' + m.nombre FROM movimiento m JOIN pokemon_movimiento_forma pmf ON pmf.id_movimiento = m.id_movimiento JOIN pokemon p ON pmf.numero_pokedex = p.numero_pokedex WHERE p.numero_pokedex = {id} FOR XML PATH('')), 1, 2, '') AS movimientos_concatenados";
            using (var connection = _conexion.ObtenerConexion())
            {
                var movimiento = await connection.QuerySingleOrDefaultAsync<Movimiento>(query);
                return movimiento;
            }
        }
        [HttpGet]
        public async Task<Tipo> GetTipoById(int id)
        {
            var query = $"SELECT t.nombre FROM tipo t JOIN pokemon_tipo pt on t.id_tipo = pt.id_tipo JOIN pokemon p ON pt.numero_pokedex = p.numero_pokedex WHERE p.id = {id}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var tipoPokemon = await connection.QuerySingleOrDefaultAsync<Tipo>(query, new { id });
                return tipoPokemon;
            }
        }

        public async Task<IEnumerable<Pokemon>> GetFilter(string filter, string direccion)
        {
            var query = $"SELECT * FROM pokemon ORDER BY {filter} {direccion}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var filteredPokemon = await connection.QueryAsync<Pokemon>(query);
                return filteredPokemon.ToList();
            }
        }        public async Task<IEnumerable<Pokemon>> GetFilterByTipo(string tipo)
        {
            if (string.IsNullOrEmpty(tipo))
            {
                return await GetPokemons();
            }
            tipo = tipo.Split(" ")[0];
            var query = $"SELECT fp.* FROM (SELECT p.*, tipo = " +
                $"STUFF((SELECT ', ' + tipo.nombre FROM tipo tipo JOIN pokemon_tipo pt ON pt.id_tipo = tipo.id_tipo " +
                $"WHERE pt.numero_pokedex = p.numero_pokedex FOR XML PATH('')), 1, 1, '') FROM pokemon p " +
                $"WHERE EXISTS(SELECT 1 FROM pokemon_tipo pt JOIN tipo t ON pt.id_tipo = t.id_tipo " +
                $"WHERE pt.numero_pokedex = p.numero_pokedex AND t.nombre LIKE '{tipo}')) " +
                $"as fp";
            using (var connection = _conexion.ObtenerConexion())
            {
                var filteredPokemon = await connection.QueryAsync<Pokemon>(query);
                return filteredPokemon.ToList();
            }
        }

        public async Task<IEnumerable<Tipo>> GetTipos(int id)
        {
            var query = $"SELECT t.nombre FROM tipo t JOIN pokemon_tipo pt ON t.id_tipo = pt.id_tipo JOIN pokemon p ON pt.numero_pokedex = p.numero_pokedex WHERE p.numero_pokedex = {id}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var tipos = await connection.QueryAsync<Tipo>(query);
                return tipos.ToList();
            }
        }
    }
}