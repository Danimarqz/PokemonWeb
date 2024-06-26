﻿using Pokemon.Models.Repository;
using Pokemon.Models;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Extensions;
using System.Text.Json;

namespace Pokemon.Controllers
{
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ITipoRepository _tipoRepository;
        private readonly IEvolucionRepository _evolucionRepository;
        public PokemonController(IPokemonRepository pokemonRepository, IMovimientoRepository movimientoRepository, ITipoRepository tipoRepository, IEvolucionRepository evolucionRepository)
        {
            _pokemonRepository = pokemonRepository;
            _movimientoRepository = movimientoRepository;
            _tipoRepository = tipoRepository;
            _evolucionRepository = evolucionRepository;
        }
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> FilterByTipo(string tipoFiltro)
        {
            var filteredPokemon = await _pokemonRepository.GetFilterByTipo(tipoFiltro);
            return View("Index", filteredPokemon);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetail(int codigo)
        {
            var pokemon = await _pokemonRepository.GetPokemonById(codigo);
            var movimientos = await _movimientoRepository.GetMovimientos(codigo);
            var tipos = await _tipoRepository.GetTipos(codigo);
            var evolucion = await _evolucionRepository.GetEvolucion(codigo);
            var origen = await _evolucionRepository.GetOrigen(codigo);
            PokeMovimiento suma = new PokeMovimiento();
            suma.pokemons = pokemon;
            suma.movimientos = movimientos;
            suma.tipos = tipos;
            if (evolucion != null)
            {
                var nameEvolucion = await _pokemonRepository.GetPokemonNameById(evolucion.pokemon_evolucionado);
                suma.pokemon_evolucionado = nameEvolucion;
            }
            if (origen != null)
            {
                var nameOrigen = await _pokemonRepository.GetPokemonNameById(origen.pokemon_origen);
                suma.pokemon_origen = nameOrigen;
            }
            

            return View("VerPokemon", suma);
        }
        public async Task<IActionResult> SavePokemon(int numPokedex)
        {
            var Data = await _pokemonRepository.GetPokemons();
            List<Models.PokeMovimiento> equipo;
            string arrayBytes = HttpContext.Session.GetString("MiEquipo");
            if (arrayBytes == null)
            {
                equipo = new List<Models.PokeMovimiento>();
            } else
            {
                equipo = JsonSerializer.Deserialize<List<Models.PokeMovimiento>>(arrayBytes);
            }
            if (equipo.Count <6)
            {
                var pokemon = await _pokemonRepository.GetPokemonById(numPokedex);
                var tipos = await _tipoRepository.GetTipos(numPokedex);
                PokeMovimiento suma = new PokeMovimiento();
                suma.pokemons = pokemon;
                suma.tipos = tipos;
                equipo.Add(suma);
                string jsonString = JsonSerializer.Serialize(equipo);
                HttpContext.Session.SetString("MiEquipo", jsonString);
                //Si es llamado desde el equipo aleatorio, debería volver a la página de equipo aleatorio?
                return View("Index", Data);
            } else
            {
              ViewBag.ErrorMessage ="Demasiados Pokémon añadidos al equipo";
              return View("Index", Data);
            }
        }
    
    }
}
