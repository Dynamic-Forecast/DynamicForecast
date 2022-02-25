
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Models
{
    public partial class DT_Usuario
    {
        public int EmpresaId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(60)]
        public string CodigoUsr { get; set; }

        [Required]
        [StringLength(120)]
        public string NombreUsr { get; set; }

        [Required]
        [StringLength(15)]
        public string NumeroNit { get; set; }

        [StringLength(250)]
        public string E_Mail { get; set; }

        [Required]
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string PalabraClave { get; set; }

        public int TipoUsuario { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(3)]
        public string CausaEstado { get; set; }

        public DateTime FechaIng { get; set; }

        public DateTime FechaMod { get; set; }



        [StringLength(1)]
        public string UsuarioAdmin { get; set; }


        public string PalabraClaveActual { get; set; }

        public string PalabraClaveNueva { get; set; }

        public string PalabraClaveNueva2 { get; set; }

        [ForeignKey("EmpresaId")]
        public virtual CT_Empresa CT_Empresa { get; set; }

        //[ForeignKey("EmpresaId")]
        //public virtual IN_Empresa IN_Empresa { get; set; }


    }
}
