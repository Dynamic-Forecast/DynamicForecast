using System;
using System.Collections.Generic;
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

        [StringLength(3)]
        public string Moneda { get; set; }

        public DateTime InicioOper { get; set; }

        public int? CierreCont { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(3)]
        public string CausaEstado { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaIng { get; set; }

        public DateTime FechaMod { get; set; }

        [StringLength(250)]
        public string Pagina { get; set; }

        public int? ProyectoId { get; set; }

        [StringLength(30)]
        public string Dominio { get; set; }

        public int Usuarios { get; set; }

        public int? ContratoId { get; set; }

        public DateTime? CierreInv { get; set; }

        public int? CierreInvt { get; set; }

        public DateTime? InicioFiscal { get; set; }

        [StringLength(1)]
        public string RemoteSvr { get; set; }

        public int? NivelProyecto { get; set; }

        [StringLength(1)]
        public string NIIF { get; set; }

        public int? ZonaId { get; set; }

        public int? Holgura { get; set; }

        [StringLength(1)]
        public string PermxBodega { get; set; }

        public int? CierreContNIIF { get; set; }

        [StringLength(1)]
        public string CostovFamilia { get; set; }

        [StringLength(1)]
        public string PararseenFecha { get; set; }

        [StringLength(1)]
        public string MenuxPermisos { get; set; }

        [StringLength(1)]
        public string ActivaTemporal { get; set; }

        [StringLength(1)]
        public string FechaAnterior { get; set; }

        [StringLength(20)]
        public string NomVariable1 { get; set; }

        [StringLength(20)]
        public string NomVariable2 { get; set; }

        [StringLength(20)]
        public string NomVariable3 { get; set; }

        [StringLength(20)]
        public string NomVariable4 { get; set; }

        [StringLength(20)]
        public string NomVariable5 { get; set; }

        [StringLength(60)]
        public string TokenEmp { get; set; }

        [StringLength(60)]
        public string TokenEmpPruebas { get; set; }



        [StringLength(60)]
        public string TokenPas { get; set; }

        [StringLength(60)]
        public string TokenPasPruebas { get; set; }




        [StringLength(1)]
        public string IndProgramaInicial { get; set; }

        [StringLength(1)]
        public string PlanCuentasNoPUC { get; set; }

        [StringLength(16)]
        public string CuentaCobrar { get; set; }

        [StringLength(1)]
        public string IndCalendario { get; set; }

        [StringLength(1)]
        public string IndPermisosTerc { get; set; }

        [StringLength(1)]
        public string ContabRemision { get; set; }

        [StringLength(60)]
        public string CodigoMail { get; set; }

        [StringLength(60)]
        public string PalabraMail { get; set; }

        [StringLength(1)]
        public string IndDashboardReservas { get; set; }

        public int? TamanioMaxAdjunto { get; set; }

        public string IdSoftwareHKA { get; set; }

        public string ActivaTestFE { get; set; }

        public string IdSoftwareHKAPruebas { get; set; }

        public string FacturaTech { get; set; }


    }
}
