using Pokemon.Models.Repository;
using Pokemon.Models;
using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics.Statistics;

namespace Pokemon.Controllers
{
    public class EquipoController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        public EquipoController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public async Task<IActionResult> Index()
        {
            var randomPokemons = _pokemonRepository.GetRandom(6);
            IEnumerable<float> pesos = randomPokemons.Select(p => p.peso);
            float media = pesos.Any() ? Statistics.Median(pesos) : 0;
            ViewBag.MediaPeso = media;
            return View("Index", randomPokemons);
        }
    }
}
