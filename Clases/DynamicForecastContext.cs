using System;
using DynamicForecast.Models;
using Microsoft.EntityFrameworkCore;
using DynamicForecast.Areas.Conductor.Models;


namespace DynamicForecast.Clases
{
    public class DynamicForecastContext : DbContext
    {
        public DynamicForecastContext(DbContextOptions<DynamicForecastContext> options) : base(options)
        { }
        public DbSet<DT_Usuario> DT_Usuario { get; set; }
        public DbSet<CT_Empresa> CT_Empresa { get; set; }
        public DbSet<DT_UsuarioXEmpr> DT_UsuarioXEmpr { get; set; }
        public DbSet<DT_Conductor> DT_Conductor { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DT_Usuario>().HasKey(c => new { c.UsuarioId });
            modelBuilder.Entity<CT_Empresa>().HasKey(c => new { c.EmpresaId });
            modelBuilder.Entity<DT_UsuarioXEmpr>().HasKey(c => new { c.UsuarioId, c.EmpresaId });
            modelBuilder.Entity<DT_Conductor>().HasKey(c => new { c.EmpresaId, c.ConductorId });

        }
    }
}
