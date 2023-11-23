namespace Pokemon.Models
{
    public class PokeData
    {
        public Pokemon pokemons { get; set; }
        public IEnumerable<Movimiento> movimientos { get; set; }
    }
}
