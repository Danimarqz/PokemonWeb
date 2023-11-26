using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Models
{
    [Table("pokemon")]
    public class Pokemon
    {
        public int numero_pokedex {  get; set; }
        public string nombre { get; set; }
        public float peso { get; set; }
        public float altura { get; set; }
        public string tipo { get; set; }
        public string pokemon_evolucionado { get; set; }
        public string pokemon_origen { get; set; }
    }
}
