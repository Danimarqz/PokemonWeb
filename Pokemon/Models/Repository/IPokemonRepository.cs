using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;

namespace Pokemon.Models.Repository
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<pokemon>> GetPokemons();
        Task<pokemon> GetPokemonById(int? id);
        Task<IEnumerable<pokemon>> GetRandom(int count);
    }
}
