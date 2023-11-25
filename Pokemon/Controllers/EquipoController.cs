﻿using Pokemon.Models.Repository;
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
            float pesoMedio = pesos.Any() ? Statistics.Median(pesos) : 0;
            IEnumerable<float> alturas = randomPokemons.Select(p => p.altura);
            float alturaMedia = alturas.Any() ? Statistics.Median(alturas) : 0;

            ViewBag.PesoMedio = pesoMedio;
            ViewBag.AlturaMedia = alturaMedia;
            return View("Index", randomPokemons);
        }
        [HttpGet]
        public async Task<IActionResult> FilterBy(string filtro)
        {
            string direccion;
            if (filtro == "peso")
            {
                direccion = dirPeso = dirPeso == null ? "ASC" : "DESC";
                dirAltura = null;
            }
            else
            {
                direccion = dirAltura = dirAltura == null ? "ASC" : "DESC";
                dirPeso = null;
            }
            ViewBag.DireccionPeso = dirPeso;
            ViewBag.DireccionAlt = dirAltura;
            var filteredPokemon = await _pokemonRepository.GetRandom(6, filtro, direccion);
            return View("Index", filteredPokemon);
        }
        static string dirPeso = null;
        static string dirAltura = null;
    }
}
