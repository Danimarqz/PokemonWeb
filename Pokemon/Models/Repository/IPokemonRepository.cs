﻿using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;

namespace Pokemon.Models.Repository
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonById(int? id);
        Task<Pokemon> GetPokemonNameById(int? id);
        Task<IEnumerable<Pokemon>> GetRandom(int count, string tipos = null, string filter = "numero_pokedex", string direccion = "");
        Task<IEnumerable<Pokemon>> GetFilter(string filter, string direccion);
        Task<IEnumerable<Pokemon>> GetFilterByTipo(string tipo);
        Task<IEnumerable<Tipo>> GetTipos(int id);
    }
}
