﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Models
{
    [Table("movimiento")]
    public class Movimiento
    {
        public int id_movimiento {  get; set; }
        public string nombre { get; set; }
        public int potencia { get; set; }
        public int precision_mov {  get; set; }
        public string descripcion { get; set; }
        public int pp {  get; set; }
        public int id_tipo { get; set; }
        public int prioridad { get; set; }
    }
}
