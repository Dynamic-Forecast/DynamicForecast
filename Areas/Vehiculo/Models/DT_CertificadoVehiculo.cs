using DynamicForecast.Areas.Certificado.Models;
using DynamicForecast.Areas.Conductor.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Vehiculo.Models
{
    public partial class DT_CertificadoVehiculo
    {
        public int EmpresaId { get; set; }

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CertificadoVehiculoId { get; set; }


        public int CertificadoId { get; set; }

        public int VehiculoId { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [ForeignKey("CertificadoId")]
        public virtual DT_Certificado DT_Certificado { get; set; }

        [ForeignKey("VehiculoId")]
        public virtual DT_Vehiculo DT_Vehiculo { get; set; }
    }
}