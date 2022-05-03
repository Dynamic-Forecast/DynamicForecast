
using DynamicForecast.Areas.Conductor.Models;
using DynamicForecast.Areas.Vehiculo.Models;
using DynamicForecast.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Viaje.Models
{
    public partial class AP_Viaje
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        // Fase de confirmación del viaje y elección de conductor y vehículo
        public DateTime? FechaInicioViaje { get; set; }

        public int? VehiculoId { get; set; }
        public int? ConductorId { get; set; }
        public int? ModeloRecomendacionId { get; set; }

        // Notificación de Fin del Viaje
        public DateTime? FechaFinalizacion { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorPagado { get; set; }

        [StringLength(920)]
        public string NovedadesViaje { get; set; }


        // Otros dartos
        //[ForeignKey("EmpresaId, UsuarioId")]
        //public virtual DT_Usuario DT_Usuario { get; set; }

        //[ForeignKey("EmpresaId, TerceroId")]
        //public virtual CT_Tercero CT_Tercero { get; set; }

        //[ForeignKey("EmpresaId, VehiculoId")]
        //public virtual DT_Vehiculo DT_Vehiculo { get; set; }

        //[ForeignKey("EmpresaId, ConductorId")]
        //public virtual DT_Conductor DT_Conductor { get; set; }

    }
}
