using DynamicForecast.Areas.Viaje.Models;
using DynamicForecast.Clases;
using System.Collections.Generic;
using System.Linq;

namespace DynamicForecast.Servicios
{
    public class ISimulacion
    {
        private readonly DynamicForecastContext FsvrConn;

        public ISimulacion(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<AP_Simulacion> GetSimulacions(int EmpresaId)
        {
            return FsvrConn.AP_Simulacion.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.SimulacionId).DefaultIfEmpty();
        }

        public IEnumerable<AP_Simulacion> GetSimulacion(int EmpresaId, int SimulacionId)
        {
            return FsvrConn.AP_Simulacion.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.SimulacionId == SimulacionId);
        }


        public void AgregarSimulacion(AP_Simulacion _Simulacion)
        {
            FsvrConn.AP_Simulacion.Add(_Simulacion);
            FsvrConn.SaveChanges();
        }

        public void ActualizarSimulacion(AP_Simulacion _Simulacion)
        {
            FsvrConn.AP_Simulacion.Update(_Simulacion);
            FsvrConn.SaveChanges();
        }

        public void EliminarSimulacion(AP_Simulacion _Simulacion)
        {
            FsvrConn.AP_Simulacion.Remove(_Simulacion);
            FsvrConn.SaveChanges();
        }

        public bool ExisteSimulacion(int EmpresaId, int SimulacionId)
        {
            var cantidad = FsvrConn.AP_Simulacion.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.SimulacionId == SimulacionId).Any();

            return cantidad;
        }
        public IEnumerable<AP_Simulacion> GetSimulacionLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.AP_Simulacion.Where(h => h.EmpresaId == EmpresaId && h.SimulacionId.ToString().Contains(busqueda));
        }
    }
}
