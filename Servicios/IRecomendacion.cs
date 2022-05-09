using DynamicForecast.Areas.Viaje.Models;
using DynamicForecast.Clases;
using System.Collections.Generic;
using System.Linq;

namespace DynamicForecast.Servicios
{
    public class IRecomendacion
    {
        private DynamicForecastContext FsvrConn;

        public IRecomendacion(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<AP_Recomendacion> GetRecomendacions(int EmpresaId)
        {
            return FsvrConn.AP_Recomendacion.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.RecomendacionId).DefaultIfEmpty();
        }

        public IEnumerable<AP_Recomendacion> GetRecomendacion(int EmpresaId, int RecomendacionId)
        {
            return FsvrConn.AP_Recomendacion.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.RecomendacionId == RecomendacionId);
        }


        public void AgregarRecomendacion(AP_Recomendacion _Recomendacion)
        {
            FsvrConn.AP_Recomendacion.Add(_Recomendacion);
            FsvrConn.SaveChanges();
        }

        public void ActualizarRecomendacion(AP_Recomendacion _Recomendacion)
        {
            FsvrConn.AP_Recomendacion.Update(_Recomendacion);
            FsvrConn.SaveChanges();
        }

        public void EliminarRecomendacion(AP_Recomendacion _Recomendacion)
        {
            FsvrConn.AP_Recomendacion.Remove(_Recomendacion);
            FsvrConn.SaveChanges();
        }

        public bool ExisteRecomendacion(int EmpresaId, int RecomendacionId)
        {
            var cantidad = FsvrConn.AP_Recomendacion.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.RecomendacionId == RecomendacionId).Any();

            return cantidad;
        }
        public IEnumerable<AP_Recomendacion> GetRecomendacionLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.AP_Recomendacion.Where(h => h.EmpresaId == EmpresaId && h.RecomendacionId.ToString().Contains(busqueda));
        }
    }
}
