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
            // Espera a que la tarea se complete y obtén el resultado
            var randomPokemons = await _pokemonRepository.GetRandom(6);

            // Extraer valores de peso de los 6 Pokémon generados
            IEnumerable<float> pesos = randomPokemons.Select(p => p.peso);

            // Calcular la mediana
            float mediana = pesos.Any() ? Statistics.Median(pesos) : 0;

            // Pasar la mediana y los 6 Pokémon a la vista
            ViewBag.MedianaPeso = mediana;
            return View("Index", randomPokemons);
        }
    }
}
