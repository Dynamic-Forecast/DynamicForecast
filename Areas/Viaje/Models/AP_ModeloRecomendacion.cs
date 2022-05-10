using DynamicForecast.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Viaje.Models
{
    public partial class AP_ModeloRecomendacion
    {
        public int EmpresaId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ModeloRecomendacionId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(200)]
        public string Tipo { get; set; }


        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }


        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        public int Precision { get; set; }

        public int Recall { get; set; }


        [StringLength(2000)]
        public string RutaArchivo { get; set; }


    }
}
