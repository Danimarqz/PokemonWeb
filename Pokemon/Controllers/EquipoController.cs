using Pokemon.Models.Repository;
using Pokemon.Models;
using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics.Statistics;
using Pokemon.Extensions;

namespace Pokemon.Controllers
{
    public class EquipoController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        public EquipoController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public async Task<List<Models.Pokemon>> GetPokemonAsync()
        {
            List<Models.Pokemon> pokemon;
            if (HttpContext.Session.GetString("SESSION") == null)
            {
                pokemon = new List<Models.Pokemon>();
                var randomPokemons = await _pokemonRepository.GetRandom(6);
                foreach (var item in randomPokemons)
                {
                    pokemon.Add(item);
                }
                //Se serializa solo al usar SetObject?
                HttpContext.Session.SetObject("SESSION", pokemon);
            }
            else
            {
                pokemon = HttpContext.Session.GetObject<List<Models.Pokemon>>("SESSION");
            }

            return pokemon;
        }
        public async Task<IActionResult> Index()
        {
            var sessionPokemons = await GetPokemonAsync();
            
            List<Models.Pokemon> pokemons = sessionPokemons;
            var tipoSeparado = transformToString(pokemons.Select(p => p.tipo).ToList());
            ViewBag.PesoMedio = pesoMedioCalc(pokemons);
            ViewBag.TipoPredominante = ObtenerValorMasFrecuente(tipoSeparado);
            ViewBag.AlturaMedia = alturaMedioCalc(pokemons);
            return View("Index", pokemons);
        }
        [HttpGet]
        public async Task<IActionResult> FilterBy(string filtro)
        {
            var sessionPokemons = await GetPokemonAsync();
            List<Models.Pokemon> filteredPokemons = sessionPokemons;
            var tipoSeparado = transformToString(sessionPokemons.Select(p => p.tipo).ToList());
            switch (filtro)
            {
                case "peso":
                    if (dirPeso == null || dirPeso == "DESC") 
                    {
                        filteredPokemons = filteredPokemons.OrderBy(p => p.peso).ToList();
                        dirPeso = "ASC";

                    } 
                    else
                    {
                        filteredPokemons = filteredPokemons.OrderByDescending(p => p.peso).ToList();
                        dirPeso = "DESC";
                    };
                    dirAltura = null;
                    break;
                case "altura":
                    if (dirAltura == null || dirAltura == "DESC")
                    {
                        filteredPokemons = filteredPokemons.OrderBy(p => p.altura).ToList();
                        dirAltura = "ASC";
                    }
                    else
                    {
                        filteredPokemons = filteredPokemons.OrderByDescending(p => p.altura).ToList();
                        dirAltura = "DESSC";
                    };
                    dirPeso = null;
                    break;

            }
            ViewBag.DireccionPeso = dirPeso;
            ViewBag.DireccionAlt = dirAltura;
            ViewBag.TipoPredominante = ObtenerValorMasFrecuente(tipoSeparado);
            ViewBag.PesoMedio = pesoMedioCalc(filteredPokemons);
            ViewBag.AlturaMedia = alturaMedioCalc(filteredPokemons);
            return View("Index", filteredPokemons);
        }
        public async Task<IActionResult> RefreshPokemons()
        {
            if (HttpContext.Session.GetString("SESSION") != null)
            {
                HttpContext.Session.Remove("SESSION");
            }
            var newPokemons = await GetPokemonAsync();
            return View("Index",newPokemons);
        }
        static string ObtenerValorMasFrecuente(List<string> lista)
        {

            var grupos = lista.GroupBy(x => x);
            var gruposOrdenados = grupos.OrderByDescending(g => g.Count());
            var grupoMasFrecuente = gruposOrdenados.First();
            string valorMasFrecuente = grupoMasFrecuente.Key;

            return valorMasFrecuente;
        }
        static float pesoMedioCalc (List<Models.Pokemon> pokemons)
        {
            IEnumerable<float> pesos = pokemons.Select(p => p.peso);
            float pesoMedio = pesos.Any() ? Statistics.Median(pesos) : 0;
            return pesoMedio;
        }
        static float alturaMedioCalc (List<Models.Pokemon> pokemons)
        {
            IEnumerable<float> alturas = pokemons.Select(p => p.altura);
            float alturaMedia = alturas.Any() ? Statistics.Median(alturas) : 0;
            return alturaMedia;
        }
        static List<string> transformToString(List<string> tipo) { 
            List<string> tipoSeparado = new List<string>();
            foreach (var i in tipo)
            {
                if (i.Contains(","))
                {
                    string[] tipe = i.Split(',');
                    tipoSeparado.Add(tipe[0]);
                    tipoSeparado.Add(tipe[1].Trim());
                }
                else
                {
                    tipoSeparado.Add(i);
                }
            }
            return tipoSeparado;
        }


        static string dirPeso = null;
        static string dirAltura = null;
    }
}