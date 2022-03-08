using DynamicForecast.Clases;
using System.Collections.Generic;
using System.Linq;
using DynamicForecast.Areas.Certificado.Models;

namespace DynamicForecast.Servicios
{
    public class ICertificado
    {
        private DynamicForecastContext FsvrConn;

        public ICertificado(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<DT_Certificado> GetCertificados(int EmpresaId)
        {
            return FsvrConn.DT_Certificado.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.CertificadoId).DefaultIfEmpty();
        }

        public IEnumerable<DT_Certificado> GetCertificado(int EmpresaId, int CertificadoId)
        {
            return FsvrConn.DT_Certificado.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.CertificadoId == CertificadoId);
        }

        public IEnumerable<DT_Certificado> GetCertificadoXCodigo(int EmpresaId, string CodCertificado)
        {
            return FsvrConn.DT_Certificado.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.CodCertificado == CodCertificado);
        }

        public void AgregarCertificado(DT_Certificado _Certificado)
        {
            FsvrConn.DT_Certificado.Add(_Certificado);
            FsvrConn.SaveChanges();
        }

        public void ActualizarCertificado(DT_Certificado _Certificado)
        {
            FsvrConn.DT_Certificado.Update(_Certificado);
            FsvrConn.SaveChanges();
        }

        public void EliminarCertificado(DT_Certificado _Certificado)
        {
            FsvrConn.DT_Certificado.Remove(_Certificado);
            FsvrConn.SaveChanges();
        }

        public bool ExisteCertificado(int EmpresaId, int CertificadoId)
        {
            var cantidad = FsvrConn.DT_Certificado.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.CertificadoId == CertificadoId).Any();

            return cantidad;
        }
        public IEnumerable<DT_Certificado> GetCertificadoLike(int EmpresaId, string busqueda)
        {
            return FsvrConn.DT_Certificado.Where(h => h.EmpresaId == EmpresaId && (h.NombreCertificado.Contains(busqueda) || h.CodCertificado.Contains(busqueda)));
        }
    }
}
