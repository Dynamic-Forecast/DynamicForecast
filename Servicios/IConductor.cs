using DynamicForecast.Clases;
using DynamicForecast.Areas.Conductor.Models;
using System.Collections.Generic;
using System.Linq;
namespace DynamicForecast.Servicios
{
    public class IConductor
    {
        private DynamicForecastContext FsvrConn;

        public IConductor(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<DT_Conductor> GetConductores(int EmpresaId)
        {
            return FsvrConn.DT_Conductor.
                            Where(h => h.EmpresaId == EmpresaId).DefaultIfEmpty().
                            OrderByDescending(h => h.ConductorId).DefaultIfEmpty();
        }

        public IEnumerable<DT_Conductor> GetConductor(int EmpresaId, int ConductorId)
        {
            return FsvrConn.DT_Conductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.ConductorId == ConductorId);
        }

        public IEnumerable<DT_Conductor> GetConductorXCodigo(int EmpresaId, string CodConductor)
        {
            return FsvrConn.DT_Conductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.CodConductor == CodConductor);
        }

        public void AgregarConductor(DT_Conductor _Conductor)
        {
            FsvrConn.DT_Conductor.Add(_Conductor);
            FsvrConn.SaveChanges();
        }

        public void ActualizarConductor(DT_Conductor _Conductor)
        {
            FsvrConn.DT_Conductor.Update(_Conductor);
            FsvrConn.SaveChanges();
        }

        public void EliminarConductor(DT_Conductor _Conductor)
        {
            FsvrConn.DT_Conductor.Remove(_Conductor);
            FsvrConn.SaveChanges();
        }

        public bool ExisteConductor(int EmpresaId, int ConductorId)
        {
            var cantidad = FsvrConn.DT_Conductor.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.ConductorId == ConductorId).Any();

            return cantidad;
        }
        public IEnumerable<DT_Conductor> GetConductorLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.DT_Conductor.Where(h => h.EmpresaId == EmpresaId && (h.NombreConductor.Contains(busqueda) || h.CodConductor.Contains(busqueda)));
        }
    }
}
