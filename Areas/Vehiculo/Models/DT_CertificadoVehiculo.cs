using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Vehiculo.Models
{
    public partial class DT_CertificadoVehiculo
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehiculoId { get; set; }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CertificadoId { get; set; }


        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

    }
}