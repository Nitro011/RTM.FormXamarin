using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models
{
    public enum MenuItemType
    {
        master,
        Browse,
        Configuraciones,
        TiposCalzados,
        GestionarModelos,
        GestionarMarcas,
        GestionarDimensiones,
        OrdenesClientes,
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
