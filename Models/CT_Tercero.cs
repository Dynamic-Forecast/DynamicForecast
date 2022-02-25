
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Models
{
    public partial class CT_Tercero
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TerceroId { get; set; }

        [Required]
        [StringLength(120)]
        public string NombreTer { get; set; }

        [Required]
        [StringLength(15)]
        public string NumeroNit { get; set; }

        [StringLength(250)]
        public string E_Mail { get; set; }

        [StringLength(120)]
        public string Direccion { get; set; }


        [StringLength(30)]
        public string Celular { get; set; }


        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(3)]
        public string CausaEstado { get; set; }

        public DateTime FechaIng { get; set; }

        public DateTime FechaMod { get; set; }


    }
}
