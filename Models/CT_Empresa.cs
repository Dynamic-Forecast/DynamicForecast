using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Models
{
    public partial class CT_Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmpresaId { get; set; }

        [Required]
        [StringLength(120)]
        public string NombreEmpr { get; set; }

        public int? TerceroId { get; set; }


        public DateTime InicioOper { get; set; }


        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(3)]
        public string CausaEstado { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaIng { get; set; }

        public DateTime FechaMod { get; set; }

    }
}
