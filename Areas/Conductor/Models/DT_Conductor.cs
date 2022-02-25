
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Areas.Conductor.Models
{
    public partial class DT_Conductor
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConductorId { get; set; }

        [Required]
        [StringLength(60)]
        public string CodConductor { get; set; }

        [Required]
        [StringLength(120)]
        public string NombreConductor { get; set; }

        [Required]
        [StringLength(15)]
        public string Documento { get; set; }

        [StringLength(250)]
        public string EMail { get; set; }

        public DateTime FechaIng { get; set; }

        public DateTime FechaMod { get; set; }

        [StringLength(120)]
        public string Direccion { get; set; }

        [StringLength(30)]
        public string Telefono { get; set; }

        [StringLength(30)]
        public string Celular { get; set; }

        [StringLength(2)]
        public string Estado { get; set; }

        [ForeignKey("EmpresaId, ConductorId")]
        public List<DT_CertificadoConductor> DT_CertificadoConductor { get; set; }

    }
}
