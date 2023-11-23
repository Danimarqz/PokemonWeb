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
            IEnumerable<float> pesos = randomPokemons.Select(p => p.pokeWeight);
            float pesoMedio = pesos.Any() ? Statistics.Median(pesos) : 0;
            IEnumerable<float> alturas = randomPokemons.Select(p => p.pokeHeight);
            float alturaMedia = alturas.Any() ? Statistics.Median(alturas) : 0;

            ViewBag.PesoMedio = pesoMedio;
            ViewBag.AlturaMedia = alturaMedia;
            return View("Index", randomPokemons);
        }
    }
}
