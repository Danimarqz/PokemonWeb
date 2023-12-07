using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Models
{
    [Table("evoluciona_de")]
    public class Evolucion
    {
        public int pokemon_evolucionado {  get; set; }
        public int pokemon_origen {  get; set; }
    }
}
