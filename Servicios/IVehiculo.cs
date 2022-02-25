using DynamicForecast.Clases;
using DynamicForecast.Areas.Vehiculo.Models;
using System.Collections.Generic;
using System.Linq;
namespace DynamicForecast.Servicios
{
    public class IVehiculo
    {
        private DynamicForecastContext FsvrConn;

        public IVehiculo(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<DT_Vehiculo> GetVehiculos(int EmpresaId)
        {
            return FsvrConn.DT_Vehiculo.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.VehiculoId).DefaultIfEmpty();
        }

        public IEnumerable<DT_Vehiculo> GetVehiculo(int EmpresaId, int VehiculoId)
        {
            return FsvrConn.DT_Vehiculo.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.VehiculoId == VehiculoId);
        }


        public void AgregarVehiculo(DT_Vehiculo _Vehiculo)
        {
            FsvrConn.DT_Vehiculo.Add(_Vehiculo);
            FsvrConn.SaveChanges();
        }

        public void ActualizarVehiculo(DT_Vehiculo _Vehiculo)
        {
            FsvrConn.DT_Vehiculo.Update(_Vehiculo);
            FsvrConn.SaveChanges();
        }

        public void EliminarVehiculo(DT_Vehiculo _Vehiculo)
        {
            FsvrConn.DT_Vehiculo.Remove(_Vehiculo);
            FsvrConn.SaveChanges();
        }

        public bool ExisteVehiculo(int EmpresaId, int VehiculoId)
        {
            var cantidad = FsvrConn.DT_Vehiculo.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.VehiculoId == VehiculoId).Any();

            return cantidad;
        }
        public IEnumerable<DT_Vehiculo> GetVehiculoLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.DT_Vehiculo.Where(h => h.EmpresaId == EmpresaId && (h.Contains(busqueda) || h.CodVehiculo.Contains(busqueda)));
        }
    }
}
