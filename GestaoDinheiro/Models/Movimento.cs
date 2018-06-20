using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestaoDinheiro.Models
{
    public class Movimento
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}")]
        public DateTime DataTime { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Display(Name = "Saldo")]
        public double Saldo_Atual { get; set; }
        public Tipo Tipo { get; set; }
        [Required]
        public string Dono { get; set; }

        public Movimento()
        {
        }
    }
}