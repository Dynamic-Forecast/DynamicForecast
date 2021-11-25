using DynamicForecast.Clases;
using DynamicForecast.Models;
using DynamicForecast.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicForecast.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly DynamicForecastContext FsvrConn;
        private Dictionary<string, string> SvrParams;
        DataSet tbEstructura;
        DataSet tbMenuDet;
        DataSet tbServicio;
        public List<string> listaEventos;
        public List<MenuItemModel> listaMenus;
        public List<MenuItemModel> listaSubMenus;

        public NavigationViewComponent(DynamicForecastContext svrConn)
        {
            FsvrConn = svrConn;
        }

        public IViewComponentResult Invoke()
        {
            listaMenus = new List<MenuItemModel>();

            if (HttpContext.Session.GetObjectFromJson<List<MenuItemModel>>("MyMenu") == null)
            {
                CambiarEmpresaFinish();
                listaMenus = listaMenus.OrderBy(h => h.Caption).ToList();
                HttpContext.Session.SetObjectAsJson("MyMenu", listaMenus);
            }
            else
            {
                listaMenus = HttpContext.Session.GetObjectFromJson<List<MenuItemModel>>("MyMenu");
            }

            return View(listaMenus);
        }

        void CambiarEmpresaFinish()
        {
            //int fEmpresaId = HttpContext.Session.GetInt32("EmpresaId") ?? 0;
            //int fUsuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            string Nombre;
            //string MenuxPermisos;
            tbEstructura = new DataSet();
            tbMenuDet = new DataSet();
            tbServicio = new DataSet();
            //Consola.Create(fEmpresaId, fUsuarioId, FsvrConn);

            // Recuperar el menu principal y la lista de servicios
            //MenuxPermisos = Consola.GetIndMenuxPermisos();

            //if (MenuxPermisos.Equals("S"))
            //{
            //    tbEstructura = Consola.GetMiMenuModXUsuarioXE(out tbMenuDet, out tbServicio);
            //}
            //else
            //{
            //    tbEstructura = Consola.GetMiMenuMod(out tbMenuDet, out tbServicio);

            //}
            SvrParams = new Dictionary<string, string>();
            foreach (DataRow row in tbServicio.Tables[0].Rows)
            {
                Nombre = row.Field<string>("NombrePort");
                SvrParams.Add(Nombre, row.Field<string>("Puerto15"));
            }

            CargarMenuEmpresa();
        }

        void CargarMenuEmpresa()
        {
            tbEstructura.Tables[0].DefaultView.Sort = "Ordena";
            foreach (DataRow fila in tbEstructura.Tables[0].Rows)
            {
                _ = new MenuItemModel
                {
                    Caption = fila.Field<string>("NombreNodo"),
                    Tag = Int32.Parse(fila.Field<string>("NodoId")),
                    IsAction = false
                };
                _ = new List<MenuItemModel>();
                CargarMenuCompleto(Int32.Parse(fila.Field<string>("NodoId")), out _, 0);
                //Item.SubItems = auxList;

            }
        }

        private void CargarMenuCompleto(int NodoPadre, out List<MenuItemModel> Menu, int NivelNodo)
        {
            //listaMenuItems = new List<MenuItemModel>();
            Menu = new List<MenuItemModel>();
            tbMenuDet.Tables[0].DefaultView.Sort = "NodoPadre, OrdenId";

            foreach (DataRow row in tbMenuDet.Tables[0].Rows)
            {
                if (Int32.Parse(row.Field<string>("NodoPadre")) == NodoPadre)
                {
                    if (Int32.Parse(row.Field<string>("ProgramaId")) == 0)
                    {
                        if (NivelNodo == 0)
                        { // Es un menu principal
                            MenuItemModel Item = new MenuItemModel
                            {
                                Caption = row.Field<string>("NombreMenu"),
                                Tag = Int32.Parse(row.Field<string>("ItemId")),
                                IsAction = false,
                                Icono = row.Field<string>("Icono")
                            };
                            _ = new List<MenuItemModel>();
                            CargarMenuCompleto(Int32.Parse(row.Field<string>("ItemId")), out List<MenuItemModel> auxList2, NivelNodo + 1);
                            Item.SubItems = auxList2;
                            listaMenus.Add(Item);
                        }
                        else
                        {
                            MenuItemModel Item = new MenuItemModel
                            {
                                Caption = row.Field<string>("NombreMenu"),
                                Tag = Int32.Parse(row.Field<string>("ItemId")),
                                IsAction = false,
                                Icono = row.Field<string>("Icono")
                            };
                            _ = new List<MenuItemModel>();
                            CargarMenuCompleto(Int32.Parse(row.Field<string>("ItemId")), out List<MenuItemModel> auxList2, NivelNodo + 1);
                            Item.SubItems = auxList2;
                            Menu.Add(Item);
                        }
                    }
                    else  // Es un Programa
                    {
                        MenuItemModel Opcion = new MenuItemModel
                        {
                            Tag = tbMenuDet.Tables[0].Rows.IndexOf(row),
                            Caption = row.Field<string>("NombreMenu"),
                            IsAction = true,
                            SubItems = null,
                            Action = row.Field<string>("Action"),
                            Controller = row.Field<string>("Controller"),
                            Parametros = row.Field<string>("ParamsWeb"),
                            Icono = row.Field<string>("Icono"),
                            Area = row.Field<string>("Area")
                        };
                        Menu.Add(Opcion);
                    }
                }
            }

        }
    }
}
