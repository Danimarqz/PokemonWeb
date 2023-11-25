using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;

namespace Pokemon.Models.Repository
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonById(int? id);
        Task<IEnumerable<Pokemon>> GetRandom(int count, string filter = "rfp.numero_pokedex", string direccion = "");
        Task<IEnumerable<Pokemon>> GetFilter(string filter, string direccion);
        Task<IEnumerable<Tipo>> GetTipos(int id);
    }
}
