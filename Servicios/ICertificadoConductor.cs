using DynamicForecast.Clases;
using DynamicForecast.Areas.Conductor.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DynamicForecast.Servicios
{
    public class ICertificadoConductor
    {
        private readonly DynamicForecastContext FsvrConn;

        public ICertificadoConductor(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<DT_CertificadoConductor> GetCertificadoConductores(int EmpresaId)
        {
            return FsvrConn.DT_CertificadoConductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.CertificadoId).DefaultIfEmpty();
        }

        public IEnumerable<DT_CertificadoConductor> GetCertificadoConductoresCompleto(int EmpresaId)
        {
            return FsvrConn.DT_CertificadoConductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            Include(h => h.DT_Conductor).AsNoTracking().DefaultIfEmpty().
                            Include(h => h.DT_Certificado).AsNoTracking().DefaultIfEmpty().
                            OrderByDescending(h => h.CertificadoId).DefaultIfEmpty();
        }

        public IEnumerable<DT_CertificadoConductor> GetCertificadoConductor(int EmpresaId, int CertificadoId, int ConductorId)
        {
            return FsvrConn.DT_CertificadoConductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.CertificadoId == CertificadoId).
                            Where(h => h.ConductorId == ConductorId);
        }
        public IEnumerable<DT_CertificadoConductor> GetCertificadosXConductor(int EmpresaId, int ConductorId)
        {
            return FsvrConn.DT_CertificadoConductor.
                            Include(h => h.DT_Certificado).
                            Include(h => h.DT_Conductor).
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.ConductorId == ConductorId);
        }

        public IEnumerable<DT_CertificadoConductor> GetCertificadosXCertificadosConductor(int EmpresaId, int CertificadoConductorId)
        {
            return FsvrConn.DT_CertificadoConductor.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.CertificadoConductorId == CertificadoConductorId);
        }


        public void AgregarCertificadoConductor(DT_CertificadoConductor _CertificadoConductor)
        {
            FsvrConn.DT_CertificadoConductor.Add(_CertificadoConductor);
            FsvrConn.SaveChanges();
        }

        public void ActualizarCertificadoConductor(DT_CertificadoConductor _CertificadoConductor)
        {
            FsvrConn.DT_CertificadoConductor.Update(_CertificadoConductor);
            FsvrConn.SaveChanges();
        }

        public void EliminarCertificadoConductor(DT_CertificadoConductor _CertificadoConductor)
        {
            FsvrConn.DT_CertificadoConductor.Remove(_CertificadoConductor);
            FsvrConn.SaveChanges();
        }

        public bool ExisteCertificadoConductor(int EmpresaId, int CertificadoConductorId)
        {
            var cantidad = FsvrConn.DT_CertificadoConductor.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.CertificadoConductorId == CertificadoConductorId).Any();

            return cantidad;
        }
        //public IEnumerable<DT_CertificadoConductor> GetCertificadoConductorLike(int EmpresaId, string busqueda)
        //{
        //    return FsvrConn.DT_CertificadoConductor.Where(h => h.EmpresaId == EmpresaId && (h.Nombr¿.Contains(busqueda) || h.CodCertificadoConductor.Contains(busqueda)));
        //}
    }
}
