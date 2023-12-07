using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;
using Pokemon.Models.Repository;
using System.Text.Json;

namespace Pokemon.Controllers
{
    public class MiEquipoController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        public MiEquipoController (IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public IActionResult Index()
        {
            List<Models.PokeMovimiento> equipo;
            string arrayBytes = HttpContext.Session.GetString("MiEquipo");
            equipo = JsonSerializer.Deserialize<List<Models.PokeMovimiento>>(arrayBytes);
            if (arrayBytes == null || equipo.Count < 1)
            {
                return View("Error");
            } else
            {
                return View("Index", equipo);
            }            
        }
        public async Task<IActionResult> DeletePokemon(int posicion)
        {
            List<Models.PokeMovimiento> equipo;
            string arrayBytes = HttpContext.Session.GetString("MiEquipo");
            equipo = JsonSerializer.Deserialize<List<Models.PokeMovimiento>>(arrayBytes);
            equipo.RemoveAt(posicion);
            string jsonString = JsonSerializer.Serialize(equipo);
            HttpContext.Session.SetString("MiEquipo", jsonString);
            return Index();
        }
    }
}
