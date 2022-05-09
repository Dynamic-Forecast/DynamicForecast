using DynamicForecast.Areas.Viaje.Models;
using DynamicForecast.Clases;
using System.Collections.Generic;
using System.Linq;

namespace DynamicForecast.Servicios
{
    public class IModeloRecomendacion
    {
        private DynamicForecastContext FsvrConn;

        public IModeloRecomendacion(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<AP_ModeloRecomendacion> GetModeloRecomendacions(int EmpresaId)
        {
            return FsvrConn.AP_ModeloRecomendacion.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.ModeloRecomendacionId).DefaultIfEmpty();
        }

        public IEnumerable<AP_ModeloRecomendacion> GetModeloRecomendacion(int EmpresaId, int ModeloRecomendacionId)
        {
            return FsvrConn.AP_ModeloRecomendacion.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.ModeloRecomendacionId == ModeloRecomendacionId);
        }

        public IEnumerable<AP_ModeloRecomendacion> GetModeloRecomendacionXNombre(int EmpresaId, string Nombre)
        {
            return FsvrConn.AP_ModeloRecomendacion.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.Nombre == Nombre);
        }

        public void AgregarModeloRecomendacion(AP_ModeloRecomendacion _ModeloRecomendacion)
        {
            FsvrConn.AP_ModeloRecomendacion.Add(_ModeloRecomendacion);
            FsvrConn.SaveChanges();
        }

        public void ActualizarModeloRecomendacion(AP_ModeloRecomendacion _ModeloRecomendacion)
        {
            FsvrConn.AP_ModeloRecomendacion.Update(_ModeloRecomendacion);
            FsvrConn.SaveChanges();
        }

        public void EliminarModeloRecomendacion(AP_ModeloRecomendacion _ModeloRecomendacion)
        {
            FsvrConn.AP_ModeloRecomendacion.Remove(_ModeloRecomendacion);
            FsvrConn.SaveChanges();
        }

        public bool ExisteModeloRecomendacion(int EmpresaId, int ModeloRecomendacionId)
        {
            var cantidad = FsvrConn.AP_ModeloRecomendacion.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.ModeloRecomendacionId == ModeloRecomendacionId).Any();

            return cantidad;
        }
        public IEnumerable<AP_ModeloRecomendacion> GetModeloRecomendacionLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.AP_ModeloRecomendacion.Where(h => h.EmpresaId == EmpresaId && (h.Descripcion.Contains(busqueda) || h.Nombre.Contains(busqueda) || h.ModeloRecomendacionId.ToString().Contains(busqueda)));
        }
    }
}
