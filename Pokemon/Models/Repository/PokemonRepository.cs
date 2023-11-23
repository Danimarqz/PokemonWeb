﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
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
            var query = "SELECT * FROM pokemon";
            using (var connection = _conexion.ObtenerConexion())
            {
                var pokemons = await connection.QueryAsync<Pokemon>(query);
                return pokemons.ToList();
            }
        }
        public async Task<Pokemon> GetPokemonById(int? id)
        {
            var query = "SELECT * FROM pokemon WHERE numero_pokedex = @id";
            using (var connection = _conexion.ObtenerConexion())
            {
                var pokemon = await connection.QuerySingleOrDefaultAsync<Pokemon>(query, new { id });
                return pokemon;
            }
        }
        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetRandom(int count)
        {
            var query = $"SELECT TOP {count} * FROM pokemon ORDER BY NEWID()";
            using (var connection = _conexion.ObtenerConexion())
            {
                var randomPokemons = await connection.QueryAsync<Pokemon>(query);
                return randomPokemons.ToList();
            }
        }
        [HttpGet]
        public async Task<Movement> GetMovimiento(int id)
        {
            var query = $"SELECT * FROM movimiento m JOIN pokemon_movimiento_forma pmf ON pmf.id_movimiento = m.id_movimiento JOIN pokemon p ON pmf.numero_pokedex = p.numero_pokedex WHERE p.numero_pokedex = {id}";
            using (var connection = _conexion.ObtenerConexion())
            {
                var movimiento = await connection.QuerySingleOrDefaultAsync<Movement>(query);
                return movimiento;
            }
        }
    }
}