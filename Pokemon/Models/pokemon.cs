using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Models
{
    [Table("pokemon")]
    public class Pokemon
    {
        [Key]
        [Column("numero_pokedex")]
        public int numero_pokedex {  get; set; }
        public string nombre { get; set; }
        public float peso { get; set; }
        public float altura { get; set; }
        public List<movimiento> movimientos { get; set; }
    }
}
