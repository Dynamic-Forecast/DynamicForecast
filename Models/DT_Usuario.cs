
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

        public int? CargoId { get; set; }

        public int? NivelUsr { get; set; }

        [StringLength(4)]
        public string CalendarId { get; set; }

        [StringLength(1)]
        public string Capacidad { get; set; }

        public int? MenuId { get; set; }

        public int? FotoId { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(3)]
        public string CausaEstado { get; set; }

        public int UsuarioAct { get; set; }

        public DateTime FechaIng { get; set; }

        public DateTime FechaMod { get; set; }

        public int? CiudadId { get; set; }

        [StringLength(120)]
        public string Direccion { get; set; }

        [StringLength(30)]
        public string Telefono { get; set; }

        [StringLength(30)]
        public string Celular { get; set; }

        [StringLength(30)]
        public string Fax { get; set; }

        public int? FirmaId { get; set; }

        [StringLength(1)]
        public string TodosProg { get; set; }

        [StringLength(1)]
        public string AccesoPC { get; set; }

        public DateTime? FechaVig { get; set; }

        public int? MapaProcId { get; set; }

        public int? PadreId { get; set; }

        public int? Autenticacion { get; set; }

        [StringLength(20)]
        public string DatoTec1 { get; set; }

        [StringLength(20)]
        public string DatoTec2 { get; set; }

        [StringLength(50)]
        public string DatoTec3 { get; set; }

        [StringLength(50)]
        public string DatoTec4 { get; set; }

        [StringLength(50)]
        public string DatoTec5 { get; set; }

        [StringLength(30)]
        public string CodigoAlt { get; set; }

        [StringLength(30)]
        public string TarjetaNro { get; set; }

        [StringLength(1)]
        public string PerfilId { get; set; }

        public int? EspecialId { get; set; }

        [StringLength(20)]
        public string Nombre1 { get; set; }

        [StringLength(20)]
        public string Nombre2 { get; set; }

        [StringLength(20)]
        public string Apellido1 { get; set; }

        [StringLength(20)]
        public string Apellido2 { get; set; }

        [StringLength(1)]
        public string UsuarioAdmin { get; set; }

        [StringLength(1)]
        public string IndDashboardPropio { get; set; }


        public string PalabraClaveActual { get; set; }

        public string PalabraClaveNueva { get; set; }

        public string PalabraClaveNueva2 { get; set; }

        [ForeignKey("EmpresaId")]
        public virtual CT_Empresa CT_Empresa { get; set; }

        //[ForeignKey("EmpresaId")]
        //public virtual IN_Empresa IN_Empresa { get; set; }


    }
}
