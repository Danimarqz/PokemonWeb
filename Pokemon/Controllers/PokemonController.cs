using Pokemon.Models.Repository;
using Pokemon.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pokemon.Controllers
{
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        public PokemonController(IPokemonRepository pokemonRepository, IMovimientoRepository movimientoRepository)
        {
            _pokemonRepository = pokemonRepository;
            _movimientoRepository = movimientoRepository;
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
        [HttpGet]
        public async Task<IActionResult> GetDetail(int codigo)
        {
            var pokemon = await _pokemonRepository.GetPokemonById(codigo);
            var movimientos = await _movimientoRepository.GetMovimientos(codigo);
            PokeData suma = new PokeData();
            suma.pokemons = pokemon;
            suma.movimientos = movimientos;
            return View("VerPokemon", suma);
        }
    
    }
}
