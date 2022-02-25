using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Conductor.Models
{
    public partial class DT_VehiculoConductor
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConductorId { get; set; }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehiculoId { get; set; }


        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

    }
}