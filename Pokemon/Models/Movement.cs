using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Models
{
    [Table("movimiento")]
    public class Movement
    {
        [Key]
        [Column("id_movimiento")]
        public int movementId {  get; set; }
        [Column("nombre")]
        public string movementName { get; set; }
        [Column("potencia")]
        public int movementPower { get; set; }
        [Column("precision_mov")]
        public int movementPrecision {  get; set; }
        [Column("descripcion")]
        public string movementDescription { get; set; }
        [Column("pp")]
        public int movementPp {  get; set; }
        [Column("id_tipo")]
        public int movementIdTipe { get; set; }
        [Column("prioridad")]
        public int movementPriority { get; set; }
    }
}
