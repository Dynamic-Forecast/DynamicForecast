using DynamicForecast.Clases;
using DynamicForecast.Areas.Vehiculo.Models;
using System.Collections.Generic;
using System.Linq;
namespace DynamicForecast.Servicios
{
    public class ICertificadoVehiculo
    {
        private DynamicForecastContext FsvrConn;

        public ICertificadoVehiculo(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IEnumerable<DT_CertificadoVehiculo> GetCertificadoVehiculos(int EmpresaId)
        {
            return FsvrConn.DT_CertificadoVehiculo.
                            Where(h => h.EmpresaId == EmpresaId).
                            OrderByDescending(h => h.CertificadoId).DefaultIfEmpty();
        }

        public IEnumerable<DT_CertificadoVehiculo> GetCertificadoVehiculo(int EmpresaId, int CertificadoId, int VehiculoId)
        {
            return FsvrConn.DT_CertificadoVehiculo.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.VehiculoId == VehiculoId).
                            Where(h => h.CertificadoId == CertificadoId);
        }
        public IEnumerable<DT_CertificadoVehiculo> GetCertificadosXVehiculo(int EmpresaId, int VehiculoId)
        {
            return FsvrConn.DT_CertificadoVehiculo.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.VehiculoId == VehiculoId);
        }
        
        public IEnumerable<DT_CertificadoVehiculo> GetCertificadosXCertificadosVehiculo(int EmpresaId, int CertificadoVehiculoId)
        {
            return FsvrConn.DT_CertificadoVehiculo.
                            Where(h => h.EmpresaId == EmpresaId).
                            Where(h => h.CertificadoVehiculoId == CertificadoVehiculoId);
        }


        public void AgregarCertificadoVehiculo(DT_CertificadoVehiculo _CertificadoVehiculo)
        {
            FsvrConn.DT_CertificadoVehiculo.Add(_CertificadoVehiculo);
            FsvrConn.SaveChanges();
        }

        public void ActualizarCertificadoVehiculo(DT_CertificadoVehiculo _CertificadoVehiculo)
        {
            FsvrConn.DT_CertificadoVehiculo.Update(_CertificadoVehiculo);
            FsvrConn.SaveChanges();
        }

        public void EliminarCertificadoVehiculo(DT_CertificadoVehiculo _CertificadoVehiculo)
        {
            FsvrConn.DT_CertificadoVehiculo.Remove(_CertificadoVehiculo);
            FsvrConn.SaveChanges();
        }

        public bool ExisteCertificadoVehiculo(int EmpresaId, int CertificadoVehiculoId)
        {
            var cantidad = FsvrConn.DT_CertificadoVehiculo.
                                    Where(h => h.EmpresaId == EmpresaId).
                                    Where(h => h.CertificadoVehiculoId == CertificadoVehiculoId).Any();

            return cantidad;
        }
        //public IEnumerable<DT_CertificadoVehiculo> GetCertificadoVehiculoLike(int EmpresaId, string busqueda)
        //{
        //    return FsvrConn.DT_CertificadoVehiculo.Where(h => h.EmpresaId == EmpresaId && (h.Nombr¿.Contains(busqueda) || h.CodCertificadoVehiculo.Contains(busqueda)));
        //}
    }
}
