using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models
{
    public enum MenuItemType
    {
        master,
        Browse,
        Empleados,
        Usuarios,
        GestionarAreasDeProduccion,
        RegistrarRoles,
        Clientes,
        Almacen,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
