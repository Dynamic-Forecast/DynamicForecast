using DynamicForecast.Areas.Certificado.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Conductor.Models
{
    public partial class DT_CertificadoConductor
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CertificadoConductorId { get; set; }
        public int CertificadoId { get; set; }

        public int ConductorId { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }


        [ForeignKey("CertificadoId")]
        public virtual DT_Certificado DT_Certificado { get; set; }

        [ForeignKey("ConductorId")]
        public virtual DT_Conductor DT_Conductor { get; set; }

    }
}