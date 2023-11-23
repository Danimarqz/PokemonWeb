using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Models
{
    [Table("tipo")]
    public class Tipo
    {
        public int id_tipo {  get; set; }
        public string nombre { get; set; }
        public int id_tipo_ataque {  get; set; }
    }
}
