namespace Pokemon.Models
{
    public class pokemovimientos
    {
        public pokemon Pokemons { get; set; }
        public IEnumerable<movimiento> Movimientos { get; set; }
    }
}
