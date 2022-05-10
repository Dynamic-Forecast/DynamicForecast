using DynamicForecast.Clases;
using DynamicForecast.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicForecast.Servicios
{
    public class IUsuario
    {
        private readonly DynamicForecastContext FsvrConn;

        public IUsuario(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<DT_Usuario> GetUsuarios(int EmpresaId)
        {
            return FsvrConn.DT_Usuario.
                Where(h => h.EmpresaId == EmpresaId).
                OrderBy(h => h.FechaIng); // Agregado 
        }

        public IEnumerable<DT_Usuario> GetUsuario(int EmpresaId, int UsuarioId)
        {
            return FsvrConn.DT_Usuario.
                   Where(h => h.EmpresaId.Equals(EmpresaId)).
                   Where(h => h.UsuarioId == UsuarioId);
        }

        public IQueryable<DT_Usuario> GetListaUsuario(int empresaId)
        {
            return FsvrConn.DT_Usuario.AsNoTracking().DefaultIfEmpty().
                   Where(h => h.EmpresaId == empresaId).
                   OrderByDescending(h => h.FechaMod);
        }

        public IEnumerable<DT_Usuario> GetUsuarioXCodigo(int EmpresaId, string CodigoUsr)
        {
            return FsvrConn.DT_Usuario.
                   Where(h => h.EmpresaId.Equals(EmpresaId)).
                   Where(h => h.CodigoUsr.Equals(CodigoUsr));
        }

        public IEnumerable<DT_Usuario> GetUsuarioLike(int EmpresaId, string usrLike)
        {
            return FsvrConn.DT_Usuario.
                   Where(h => h.EmpresaId.Equals(EmpresaId) && (h.NumeroNit.Contains(usrLike) || h.NombreUsr.Contains(usrLike)));
        }

        public void AgregarUsuario(DT_Usuario _Usuario)
        {
            FsvrConn.DT_Usuario.Add(_Usuario);
            FsvrConn.SaveChanges();
        }

        public void ActualizarUsuario(DT_Usuario _Usuario)
        {
            FsvrConn.DT_Usuario.Update(_Usuario);
            FsvrConn.SaveChanges();
        }

        public void EliminarUsuario(DT_Usuario _Usuario)
        {
            FsvrConn.DT_Usuario.Remove(_Usuario);
            FsvrConn.SaveChanges();
        }

    }
}
