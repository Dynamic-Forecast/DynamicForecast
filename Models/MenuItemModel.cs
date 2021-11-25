using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicForecast.Models
{
    public class MenuItemModel
    {
        public string Caption { get; set; }
        public int Tag { get; set; }
        public int ItemId { get; set; }
        public string OnClick { get; set; }
        public string Icono { get; set; }
        public string Parametros { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int? NodoPadre { get; set; }
        public List<MenuItemModel> SubItems { get; set; }
        public bool IsAction { get; set; }
        public string Area { get; set; }
        /*public int MenuID { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        public string Link { get; set; }
        public string Class { get; set; }
        public List SubMenu { get; set; }*/

    }
}