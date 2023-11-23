using Dapper;

namespace Pokemon.Models.Repository
{
    public class TipoRepository : ITipoRepository
    {
        private readonly Conexion _conexion;

        public TipoRepository(Conexion conexion)
        {
            _conexion = conexion;
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
