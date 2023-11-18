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
            var randomPokemons = await _pokemonRepository.GetRandom(6);
            IEnumerable<float> pesos = randomPokemons.Select(p => p.peso);
            float mediana = pesos.Any() ? Statistics.Median(pesos) : 0;

            ViewBag.MedianaPeso = mediana;
            return View("Index", randomPokemons);
        }
    }
}
