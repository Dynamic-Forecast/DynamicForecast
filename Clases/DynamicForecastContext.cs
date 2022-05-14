using System;
using DynamicForecast.Models;
using Microsoft.EntityFrameworkCore;
using DynamicForecast.Areas.Conductor.Models;
using DynamicForecast.Areas.Vehiculo.Models;
using DynamicForecast.Areas.Certificado.Models;
using DynamicForecast.Areas.Viaje.Models;

namespace DynamicForecast.Clases
{
    public class DynamicForecastContext : DbContext
    {
        public DynamicForecastContext(DbContextOptions<DynamicForecastContext> options) : base(options)
        { }
        public DbSet<DT_Usuario> DT_Usuario { get; set; }
        public DbSet<CT_Empresa> CT_Empresa { get; set; }
        public DbSet<DT_UsuarioXEmpr> DT_UsuarioXEmpr { get; set; }
        public DbSet<CT_Tercero> CT_Tercero { get; set; }
        public DbSet<DT_Conductor> DT_Conductor { get; set; }
        public DbSet<DT_Certificado> DT_Certificado { get; set; }
        public DbSet<DT_CertificadoConductor> DT_CertificadoConductor { get; set; }
        public DbSet<DT_Vehiculo> DT_Vehiculo { get; set; }
        public DbSet<DT_VehiculoConductor> DT_VehiculoConductor { get; set; }
        public DbSet<DT_CertificadoVehiculo> DT_CertificadoVehiculo { get; set; }
        public DbSet<AP_Viaje> AP_Viaje { get; set; }
        public DbSet<AP_Simulacion> AP_Simulacion { get; set; }
        public DbSet<AP_Recomendacion> AP_Recomendacion { get; set; }
        public DbSet<AP_ModeloRecomendacion> AP_ModeloRecomendacion { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DT_Usuario>().HasKey(c => new {  c.UsuarioId });
            modelBuilder.Entity<CT_Empresa>().HasKey(c => new { c.EmpresaId });
            modelBuilder.Entity<DT_UsuarioXEmpr>().HasKey(c => new {  c.UsuarioXEmpresaId });
            modelBuilder.Entity<CT_Tercero>().HasKey(c => new {  c.TerceroId });
            modelBuilder.Entity<DT_Conductor>().HasKey(c => new {  c.ConductorId });
            modelBuilder.Entity<DT_Certificado>().HasKey(c => new { c.CertificadoId });
            modelBuilder.Entity<DT_CertificadoConductor>().HasKey(c => new {  c.CertificadoConductorId });
            modelBuilder.Entity<DT_Vehiculo>().HasKey(c => new {c.VehiculoId });
            modelBuilder.Entity<DT_VehiculoConductor>().HasKey(c => new {c.VehiculoConductorId});
            modelBuilder.Entity<DT_CertificadoVehiculo>().HasKey(c => new {  c.CertificadoVehiculoId });
            modelBuilder.Entity<AP_Viaje>().HasKey(c => new { c.ViajeId });
            modelBuilder.Entity<AP_Simulacion>().HasKey(c => new {c.SimulacionId });
            modelBuilder.Entity<AP_Recomendacion>().HasKey(c => new {  c.RecomendacionId });
            modelBuilder.Entity<AP_ModeloRecomendacion>().HasKey(c => new { c.ModeloRecomendacionId });

        }
    }
}
