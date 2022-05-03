using DynamicForecast.Areas.Conductor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Vehiculo.Models
{
    public partial class DT_Vehiculo
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehiculoId { get; set; }

        [Required]
        [StringLength(20)]
        public string CodPlacas { get; set; }

        [Required]
        [StringLength(250)]
        public string Modelo { get; set; }

        [Required]
        [StringLength(250)]
        public string MarcaEmpresa { get; set; }

        [Required]
        [StringLength(20)]
        public string CodConfiguracion { get; set; }

        [Required]
        [StringLength(250)]
        public string NaturalezaCarga { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoMecanico { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoLegal { get; set; }

        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        public int Capacidad { get; set; } //Galones / Toneladas}

        [Required]
        public int CapacidadVolumen { get; set; } //M3


        //[ForeignKey("EmpresaId, VehiculoId")]
        //public List<DT_CertificadoVehiculo> DT_CertificadoVehiculo { get; set; }

        //[ForeignKey("EmpresaId, VehiculoId")]
        //public List<DT_VehiculoConductor> DT_VehiculoConductor { get; set; }

    }
}