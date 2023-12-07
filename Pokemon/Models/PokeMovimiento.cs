namespace Pokemon.Models
{
    public class PokeMovimiento
    {
        public Pokemon pokemons { get; set; }
        public IEnumerable<Movimiento> movimientos { get; set; }
        public IEnumerable<Tipo> tipos { get; set; }
        public Pokemon pokemon_evolucionado { get; set; }
        public Pokemon pokemon_origen { get; set; }
    }
}
