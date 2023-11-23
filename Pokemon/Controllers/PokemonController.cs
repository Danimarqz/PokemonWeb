using Pokemon.Models.Repository;
using Pokemon.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pokemon.Controllers
{
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ITipoRepository _tipoRepository;
        public PokemonController(IPokemonRepository pokemonRepository, IMovimientoRepository movimientoRepository, ITipoRepository tipoRepository)
        {
            _pokemonRepository = pokemonRepository;
            _movimientoRepository = movimientoRepository;
            _tipoRepository = tipoRepository;
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
        public async Task<IActionResult> FilterBy(string filtro)
        {
            string direccion;
            if (filtro == "peso")
            {
                direccion = dirPeso = dirPeso == null ? "ASC" : "DESC";
                dirAltura = null;
            } else
            {
                direccion = dirAltura = dirAltura == null ? "ASC" : "DESC";
                dirPeso = null;
            }
            ViewBag.DireccionPeso = dirPeso;
            ViewBag.DireccionAlt = dirAltura;
            var filteredPokemon = await _pokemonRepository.GetFilter(filtro, direccion);
            return View("Index", filteredPokemon);
        }
        static string dirPeso = null;
        static string dirAltura = null;
    [HttpGet]
        public async Task<IActionResult> GetDetail(int codigo)
        {
            var pokemon = await _pokemonRepository.GetPokemonById(codigo);
            var movimientos = await _movimientoRepository.GetMovimientos(codigo);
            PokeMovimiento suma = new PokeMovimiento();
            suma.pokemons = pokemon;
            suma.movimientos = movimientos;

            return View("VerPokemon", suma);
        }
    
    }
}
