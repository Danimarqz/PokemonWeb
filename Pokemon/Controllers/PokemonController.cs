using Pokemon.Models.Repository;
using Pokemon.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pokemon.Controllers
{
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        public PokemonController(IPokemonRepository pokemonRepository) 
        {
           _pokemonRepository = pokemonRepository;
        }
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Data = await _pokemonRepository.GetPokemons();
            return View("Index", Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int codigo)
        {
            var pokemon = await _pokemonRepository.GetPokemonById(codigo);
            return View("VerPokemon", pokemon);
        }
    }
}
