using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.FormXamarin.Models
{
    public enum MenuItemType
    {
        PaginaPrincipal,
        Browse,
        Configuraciones,
        TiposCalzados,
        GestionarClientes,
        GestionarModelos,
        GestionarMarcas,
        GestionarDimensiones,
        GestionarControlUbicacionPiezas,
        OrdenesClientes,
        Clientes,
        OperacionesCalzados,
        BOM,
        Almacen,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
