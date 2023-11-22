﻿using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;

namespace Pokemon.Models.Repository
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonById(int? id);
        Task<IEnumerable<Pokemon>> GetRandom(int count);
    }
}
