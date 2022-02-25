
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Models
{
    public partial class DT_UsuarioXEmpr
    {
        [Key]
        [Column(Order = 0)]
        public int UsuarioXEmpresaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UsuarioId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmpresaId { get; set; }

        public int UsuarioAct { get; set; }

        public DateTime FechaIng { get; set; }


        [ForeignKey("EmpresaId")]
        public virtual CT_Empresa CT_Empresa { get; set; }


        [ForeignKey("UsuarioId")]
        public virtual DT_Usuario DT_Usuario { get; set; }

    }
}
