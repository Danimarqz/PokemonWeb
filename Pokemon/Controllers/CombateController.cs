using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;
using Pokemon.Models.Repository;
using System.Text.Json;

namespace Pokemon.Controllers
{
    public class CombateController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        public CombateController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CombateEquipo()
        {
            List<Models.PokeMovimiento> equipo;
            string arrayBytes = HttpContext.Session.GetString("MiEquipo");
            if (arrayBytes == null)
            {
                return View("../MiEquipo/Error");
            }
            equipo = JsonSerializer.Deserialize<List<Models.PokeMovimiento>>(arrayBytes);
            var equipocombate = await _pokemonRepository.GetRandom(6);
            List<Pokemon.Models.Pokemon> listaEquipo1 = equipocombate.ToList();
            int contador = 0;
            string[] ganador = new string[6];
            for (int i = 0; i < equipo.Count();)
            {
                for (int j = 0; j < equipocombate.Count() && i < equipo.Count();)
                {
                    string winner = CheckWinner(equipo[i], listaEquipo1[j]);
                    //Console.WriteLine($"{equipo[i].pokemons.nombre} vs {listaEquipo1[j].nombre} - {winner}");

                    if (winner == "pokemon1")
                    {
                        j++;
                        ganador[contador] = winner;
                        contador++;
                    }
                    else
                    {
                        i++;
                        ganador[contador] = winner;
                        contador++;
                    }
                }
            }
            if (ganador.Count(s => s == "pokemon1") > ganador.Count(s => s == "pokemon2"))
            {
                return View("Team1win");
            }
            else if (ganador.Count(s => s == "pokemon2") > ganador.Count(s => s == "pokemon1"))
            {
                return View("Team2win");
            }
            else { return View("Empate"); }
        }


        public async Task<IActionResult> CombateAleatorio()
        {

            var equipocombate = await _pokemonRepository.GetRandom(6);
            List<Pokemon.Models.Pokemon> listaEquipo1 = equipocombate.ToList();
            var equipocombate2 = await _pokemonRepository.GetRandom(6);
            List<Pokemon.Models.Pokemon> listaEquipo2 = equipocombate2.ToList();
            string[] ganador = new string[12];
            int contador = 0;
            for (int i = 0; i < equipocombate.Count();)
            {
                for (int j = 0; j < equipocombate2.Count() && i < equipocombate.Count(); )
                {
                    string winner = CheckWinner(listaEquipo1[i], listaEquipo2[j]);
                    //Console.WriteLine($"{listaEquipo1[i].nombre} vs {listaEquipo2[j].nombre}: Winner - {winner}");

                    if (winner == "pokemon1")
                    {
                        j++;
                        ganador[contador] = winner;
                        contador++;
                    } else
                    {
                        i++;
                        ganador[contador] = winner;
                        contador++;
                    }
                }
            }

            if (ganador.Count(s => s == "pokemon1") > ganador.Count(s => s == "pokemon2"))
            {
                return View("Team1win");
            }
            else if (ganador.Count(s => s == "pokemon2") > ganador.Count(s => s == "pokemon1"))
            {
                return View("Team2win");
            }
            else { return View("Empate"); }
        }

        private static string CheckWinner(PokeMovimiento pokemon2, Models.Pokemon pokemon)
        {
            string tipo1 = pokemon.tipo.Split(", ")[0].Trim();
            string tipo2 = pokemon2.tipos.ToString();
            switch (tipo1)
            {
                case "Acero":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Tierra")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Agua":
                    {
                        if (tipo2 == "Planta" || tipo2 == "Eléctrico")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Bicho":
                    {
                        if (tipo2 == "Volador" || tipo2 == "Fuego" || tipo2 == "Roca")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Dragón":
                    {
                        if (tipo2 == "Hada" || tipo2 == "Hielo" || tipo2 == "Dragón")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Eléctrico":
                    {
                        if (tipo2 == "Tierra")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Fantasma":
                    {
                        if (tipo2 == "Fantasma" || tipo2 == "Siniestro")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Fuego":
                    {
                        if (tipo2 == "Tierra" || tipo2 == "Agua" || tipo2 == "Roca")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Hada":
                    {
                        if (tipo2 == "Acero" || tipo2 == "Veneno")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Hielo":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Acero" || tipo2 == "Roca" || tipo2 == "Fuego")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Lucha":
                    {
                        if (tipo2 == "Psíquico" || tipo2 == "Volador" || tipo2 == "Hielo")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Normal":
                    {
                        if (tipo2 == "Lucha")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Planta":
                    {
                        if (tipo2 == "Volador" || tipo2 == "Bicho" || tipo2 == "Veneno" || tipo2 == "Hielo" || tipo2 == "Fuego")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Psíquico":
                    {
                        if (tipo2 == "Bicho" || tipo2 == "Fantasma" || tipo2 == "Siniestro")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Roca":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Tierra" || tipo2 == "Acero" || tipo2 == "Agua" || tipo2 == "Planta")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Siniestro":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Hada" || tipo2 == "Bicho")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Tierra":
                    {
                        if (tipo2 == "Agua" || tipo2 == "Planta" || tipo2 == "Hielo")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Veneno":
                    {
                        if (tipo2 == "Tierra" || tipo2 == "Psíquico")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Volador":
                    {
                        if (tipo2 == "Roca" || tipo2 == "Hielo" || tipo2 == "Eléctrico")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                default: return "Error";
            }
        }
        private static string CheckWinner(Pokemon.Models.Pokemon pokemon, Pokemon.Models.Pokemon pokemon2)
        {
            string tipo1 = pokemon.tipo.Split(", ")[0].Trim();
            string tipo2 = pokemon2.tipo.Split(", ")[0].Trim();
            switch (tipo1)
            {
                case "Acero":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Tierra")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Agua":
                    {
                        if (tipo2 == "Planta" || tipo2 == "Eléctrico")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Bicho":
                    {
                        if (tipo2 == "Volador" || tipo2 == "Fuego" || tipo2 == "Roca")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Dragón":
                    {
                        if (tipo2 == "Hada" || tipo2 == "Hielo" || tipo2 == "Dragón")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Eléctrico":
                    {
                        if (tipo2 == "Tierra")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Fantasma":
                    {
                        if (tipo2 == "Fantasma" || tipo2 == "Siniestro")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Fuego":
                    {
                        if (tipo2 == "Tierra" || tipo2 == "Agua" || tipo2 == "Roca")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Hada":
                    {
                        if (tipo2 == "Acero" || tipo2 == "Veneno")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Hielo":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Acero" || tipo2 == "Roca" || tipo2 == "Fuego")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Lucha":
                    {
                        if (tipo2 == "Psíquico" || tipo2 == "Volador" || tipo2 == "Hielo")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Normal":
                    {
                        if (tipo2 == "Lucha")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Planta":
                    {
                        if (tipo2 == "Volador" || tipo2 == "Bicho" || tipo2 == "Veneno" || tipo2 == "Hielo" || tipo2 == "Fuego")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Psíquico":
                    {
                        if (tipo2 == "Bicho" || tipo2 == "Fantasma" || tipo2 == "Siniestro")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Roca":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Tierra" || tipo2 == "Acero" || tipo2 == "Agua" || tipo2 == "Planta")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Siniestro":
                    {
                        if (tipo2 == "Lucha" || tipo2 == "Hada" || tipo2 == "Bicho")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Tierra":
                    {
                        if (tipo2 == "Agua" || tipo2 == "Planta" || tipo2 == "Hielo")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Veneno":
                    {
                        if (tipo2 == "Tierra" || tipo2 == "Psíquico")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                case "Volador":
                    {
                        if (tipo2 == "Roca" || tipo2 == "Hielo" || tipo2 == "Eléctrico")
                        {
                            return "pokemon1";
                        }
                        else { return "pokemon2"; }
                    }
                default: return "Error";
            }
        }
    }
}
