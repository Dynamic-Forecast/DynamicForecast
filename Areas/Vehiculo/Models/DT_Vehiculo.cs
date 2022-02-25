using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Vehiculo.Models
{
    public partial class DT_Vehiculo
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConductorId { get; set; }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CertificadoId { get; set; }

        [Required]
        [StringLength(60)]
        public string CodCertificado { get; set; }

        [Required]
        [StringLength(120)]
        public string NombreCertificado { get; set; }

        [Required]
        [StringLength(15)]
        public string TipoCertificado { get; set; }

        [Required]
        [StringLength(250)]
        public string DescripcionCertificado { get; set; }

        [Required]
        public DateTime FechaIng { get; set; }

        [Required]
        public DateTime FechaMod { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

    }
}