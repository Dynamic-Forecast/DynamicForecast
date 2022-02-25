using DynamicForecast.Clases;
using DynamicForecast.Models;
using System.Collections.Generic;
using System.Linq;
namespace DynamicForecast.Servicios
{
    public class ITercero
    {
        private DynamicForecastContext FsvrConn;

        public ITercero(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<CT_Tercero> GetTerceroes(int EmpresaId)
        {
            return FsvrConn.CT_Tercero.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.TerceroId).DefaultIfEmpty();
        }

        public IEnumerable<CT_Tercero> GetTercero(int EmpresaId, int TerceroId)
        {
            return FsvrConn.CT_Tercero.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.TerceroId == TerceroId);
        }

        public IEnumerable<CT_Tercero> GetTerceroXNit(int EmpresaId, string NitTercero)
        {
            return FsvrConn.CT_Tercero.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.NumeroNit == NitTercero);
        }

        public void AgregarTercero(CT_Tercero _Tercero)
        {
            FsvrConn.CT_Tercero.Add(_Tercero);
            FsvrConn.SaveChanges();
        }

        public void ActualizarTercero(CT_Tercero _Tercero)
        {
            FsvrConn.CT_Tercero.Update(_Tercero);
            FsvrConn.SaveChanges();
        }

        public void EliminarTercero(CT_Tercero _Tercero)
        {
            FsvrConn.CT_Tercero.Remove(_Tercero);
            FsvrConn.SaveChanges();
        }

        public bool ExisteTercero(int EmpresaId, int TerceroId)
        {
            var cantidad = FsvrConn.CT_Tercero.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.TerceroId == TerceroId).Any();

            return cantidad;
        }
        public IEnumerable<CT_Tercero> GetTerceroLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.CT_Tercero.Where(h => h.EmpresaId == EmpresaId && (h.NombreTer.Contains(busqueda) || h.NumeroNit.Contains(busqueda)));
        }
    }
}
