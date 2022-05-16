using DynamicForecast.Areas.Viaje.Models;
using DynamicForecast.Clases;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DynamicForecast.Servicios
{
    public class IViaje
    {
        private readonly DynamicForecastContext FsvrConn;

        public IViaje(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<AP_Viaje> GetViajes(int EmpresaId)
        {
            return FsvrConn.AP_Viaje.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.ViajeId).DefaultIfEmpty();
        }

        public IEnumerable<AP_Viaje> GetViaje(int EmpresaId, int ViajeId)
        {
            return FsvrConn.AP_Viaje.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.ViajeId == ViajeId);
        }
        public IEnumerable<AP_Viaje> GetViajeCompleto(int EmpresaId, int ViajeId)
        {
            return FsvrConn.AP_Viaje.
                            Include(h => h.CT_Tercero).AsNoTracking().DefaultIfEmpty().
                            Include(h => h.DT_Usuario).AsNoTracking().DefaultIfEmpty().
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.ViajeId == ViajeId);
        }

        public IEnumerable<AP_Viaje> GetViajeXCodigo(int EmpresaId, string CodViaje)
        {
            return FsvrConn.AP_Viaje.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.CodViaje == CodViaje);
        }

        public void AgregarViaje(AP_Viaje _Viaje)
        {
            FsvrConn.AP_Viaje.Add(_Viaje);
            FsvrConn.SaveChanges();
        }

        public void ActualizarViaje(AP_Viaje _Viaje)
        {
            FsvrConn.AP_Viaje.Update(_Viaje);
            FsvrConn.SaveChanges();
        }

        public void EliminarViaje(AP_Viaje _Viaje)
        {
            FsvrConn.AP_Viaje.Remove(_Viaje);
            FsvrConn.SaveChanges();
        }

        public bool ExisteViaje(int EmpresaId, int ViajeId)
        {
            var cantidad = FsvrConn.AP_Viaje.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.ViajeId == ViajeId).Any();

            return cantidad;
        }
        public IEnumerable<AP_Viaje> GetViajeLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.AP_Viaje.Where(h => h.EmpresaId == EmpresaId && (h.Descripcion.Contains(busqueda) || h.CodViaje.Contains(busqueda) || h.ViajeId.ToString().Contains(busqueda)));
        }
    }
}
