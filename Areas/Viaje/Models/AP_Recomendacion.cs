using DynamicForecast.Areas.Conductor.Models;
using DynamicForecast.Areas.Vehiculo.Models;
using DynamicForecast.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Viaje.Models
{
    public partial class AP_Recomendacion
    {
        public int EmpresaId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SimulacionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecomendacionId { get; set; }


        [Required]
        public int ViajeId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int TerceroId { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(200)]
        public string TipoRecomendacion { get; set; }



        public int? VehiculoId { get; set; }

        public int? ConductorId { get; set; }

        public int? ModeloRecomendacionId { get; set; }

        // Otros datos

    }
}
