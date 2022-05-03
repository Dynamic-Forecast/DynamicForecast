using DynamicForecast.Areas.Certificado.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Conductor.Models
{
    public partial class DT_CertificadoConductor
    {
        public int EmpresaId { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CertificadoConductorId { get; set; }
        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CertificadoId { get; set; }

        [Key, Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConductorId { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        public DateTime FechaCertificado { get; set; }

        public DateTime FechaVencimientoCertificado { get; set; }

        [StringLength(850)]
        public string UrlCertificado { get; set; }

        [ForeignKey("EmpresaId, CertificadoId")]
        public DT_Certificado DT_Certificado { get; set; }

        [ForeignKey("EmpresaId, ConductorId")]
        public DT_Conductor DT_Conductor { get; set; }

    }
}