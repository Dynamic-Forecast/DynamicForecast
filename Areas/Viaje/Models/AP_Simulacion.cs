
using DynamicForecast.Areas.Conductor.Models;
using DynamicForecast.Areas.Vehiculo.Models;
using DynamicForecast.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Viaje.Models
{
    public partial class AP_Simulacion
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SimulacionId { get; set; }

        public int ViajeId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int TerceroId { get; set; }

        [Required]
        [StringLength(60)]
        public string CodViaje { get; set; }

        [Required]
        [StringLength(120)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        public DateTime FechaServicio { get; set; }


        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorCotizado { get; set; }

        [Required]
        [StringLength(20)]
        public string CodOperacionTransporte { get; set; }

        [Required]
        [StringLength(20)]
        public string CodMunicipioOrigen { get; set; }

        [Required]
        [StringLength(20)]
        public string CodMunicipioDestino { get; set; }

        [Required]
        [StringLength(20)]
        public string CodDepartamentoOrigen { get; set; }

        [Required]
        [StringLength(20)]
        public string CodDepartamentoDestino { get; set; }

        [Required]
        [StringLength(20)]
        public string CodMercancia { get; set; }

        [Required]
        [StringLength(250)]
        public string NaturalezaCarga { get; set; }

        [Required]
        public int Kilometros { get; set; }

        [Required]
        public int Contenido { get; set; } // (Peso) Galones - Toneladas

        [Required]
        public int Volumen { get; set; } //M3

        [Required]
        public int PorcentajeRutaPrimaria { get; set; }

        [Required]
        public int PorcentajeRutaSecundaria { get; set; }

        [Required]
        public int PorcentajeRutaTerciaria { get; set; }

        [Required]
        public int NumeroPeajes { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostoPeajes { get; set; }


        public int? ModeloRecomendacionId { get; set; }


        public int? CalificacionRecomendacion { get; set; }


        [StringLength(1200)]
        public string ComentarioSimulacion { get; set; }

        [StringLength(1)]
        public string SimulacionElegida { get; set; }


        [ForeignKey("EmpresaId, RecomendacionId")]
        public virtual List<AP_Recomendacion> AP_Recomendacion { get; set; }

 

    }
}
