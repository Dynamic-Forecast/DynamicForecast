using DynamicForecast.Clases;
using DynamicForecast.Areas.Conductor.Models;
using System.Collections.Generic;
using System.Linq;
namespace DynamicForecast.Servicios
{
    public class IVehiculoConductor
    {
        private DynamicForecastContext FsvrConn;

        public IVehiculoConductor(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<DT_VehiculoConductor> GetVehiculoConductores(int EmpresaId)
        {
            return FsvrConn.DT_VehiculoConductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.VehiculoId).DefaultIfEmpty();
        }

        public IEnumerable<DT_VehiculoConductor> GetVehiculoConductor(int EmpresaId, int VehiculoConductorId)
        {
            return FsvrConn.DT_VehiculoConductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.VehiculoId == VehiculoConductorId);
        }


        public void AgregarVehiculoConductor(DT_VehiculoConductor _VehiculoConductor)
        {
            FsvrConn.DT_VehiculoConductor.Add(_VehiculoConductor);
            FsvrConn.SaveChanges();
        }

        public void ActualizarVehiculoConductor(DT_VehiculoConductor _VehiculoConductor)
        {
            FsvrConn.DT_VehiculoConductor.Update(_VehiculoConductor);
            FsvrConn.SaveChanges();
        }

        public void EliminarVehiculoConductor(DT_VehiculoConductor _VehiculoConductor)
        {
            FsvrConn.DT_VehiculoConductor.Remove(_VehiculoConductor);
            FsvrConn.SaveChanges();
        }

        public bool ExisteVehiculoConductor(int EmpresaId, int VehiculoConductorId)
        {
            var cantidad = FsvrConn.DT_VehiculoConductor.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.VehiculoId == VehiculoConductorId).Any();

            return cantidad;
        }
        //public IEnumerable<DT_VehiculoConductor> GetVehiculoConductorLike(int EmpresaId, string busqueda)
        //{
        //    return FsvrConn.DT_VehiculoConductor.Where(h => h.EmpresaId == EmpresaId && (h.Nombr¿.Contains(busqueda) || h.CodVehiculoConductor.Contains(busqueda)));
        //}
    }
}
