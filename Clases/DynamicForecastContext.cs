using System;
using DynamicForecast.Models;
using Microsoft.EntityFrameworkCore;
//using EqualTech.Models;
//using EqualTech.Areas.Administracion.Models;


namespace DynamicForecast.Clases
{
    public class DynamicForecastContext : DbContext
    {
        public DynamicForecastContext(DbContextOptions<DynamicForecastContext> options) : base(options)
        { }
        public DbSet<DT_Usuario> DT_Usuario { get; set; }
        public DbSet<CT_Empresa> CT_Empresa { get; set; }
        public DbSet<DT_UsuarioXEmpr> DT_UsuarioXEmpr { get; set; }
        //public DbSet<IN_ProductoCategoria> IN_ProductoCategoria { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DT_Usuario>().HasKey(c => new { c.UsuarioId });
            modelBuilder.Entity<CT_Empresa>().HasKey(c => new { c.EmpresaId });
            modelBuilder.Entity<DT_UsuarioXEmpr>().HasKey(c => new { c.UsuarioId, c.EmpresaId });

            //modelBuilder.Entity<IN_ProductoCategoria>().HasKey(c => new { c.EmpresaId, c.ProductoId, c.CategoriaId, c.SubCategoriaId });

        }
    }
}
