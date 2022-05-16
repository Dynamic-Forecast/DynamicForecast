
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

        public int UsuarioId { get; set; }

        public int EmpresaId { get; set; }

        public int UsuarioAct { get; set; }

        public DateTime FechaIng { get; set; }


        [ForeignKey("EmpresaId")]
        public virtual CT_Empresa CT_Empresa { get; set; }


        [ForeignKey("UsuarioId")]
        public virtual DT_Usuario DT_Usuario { get; set; }

    }
}
