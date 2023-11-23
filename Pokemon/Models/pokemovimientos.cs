namespace Pokemon.Models
{
    public class pokemovimientos
    {
        public Pokemon Pokemons { get; set; }
        public IEnumerable<Movement> Moves { get; set; }
    }
}
