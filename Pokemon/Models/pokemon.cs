using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Models
{
    [Table("pokemon")]
    public class Pokemon
    {
        [Key]
        [Column("numero_pokedex")]
        public int numPokedex {  get; set; }
        [Column("nombre")]
        public string pokeName { get; set; }
        [Column("peso")]
        public float pokeWeight { get; set; }
        [Column("altura")]
        public float pokeHeight { get; set; }
    }
}
