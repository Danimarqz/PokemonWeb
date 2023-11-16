using Dapper;
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

        public async Task<IEnumerable<pokemon>> GetPokemons()
        {
            var query = "SELECT * FROM pokemon";
            using (var connection = _conexion.ObtenerConexion())
            {
                var pokemons = await connection.QueryAsync<pokemon>(query);
                return pokemons.ToList();
            }
        }
        public async Task<pokemon> GetPokemonById(int? id)
        {
            var query = "SELECT * FROM pokemon WHERE numero_pokedex = @id";
            using (var connection = _conexion.ObtenerConexion())
            {
                var pokemon = await connection.QuerySingleOrDefaultAsync<pokemon>(query, new { id });
                return pokemon;
            }
        }
    }
}