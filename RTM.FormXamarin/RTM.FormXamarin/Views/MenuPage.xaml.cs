using RTM.FormXamarin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.PaginaPrincipal, Title="Pagina Principal" },
                new HomeMenuItem {Id = MenuItemType.GestionHumana, Title="Gestion Humana"},
                new HomeMenuItem {Id = MenuItemType.Ingenieria, Title="Ingenieria"},
                new HomeMenuItem {Id = MenuItemType.GestionEstilo, Title="Gestion de Estilos"},
                new HomeMenuItem {Id = MenuItemType.GestionOrdenesPO, Title="Gestion de Ordenes PO"},
                new HomeMenuItem {Id = MenuItemType.GestionProduccion, Title="Gestion de Produccion"},
                new HomeMenuItem {Id = MenuItemType.GestionMateriales, Title="Gestion de Materiales"},
                new HomeMenuItem {Id= MenuItemType.GestionUsuario, Title="Gestion de Usuarios"},
                new HomeMenuItem {Id = MenuItemType.Configuraciones, Title="Configuraciones"},
                new HomeMenuItem {Id = MenuItemType.GestionarControlUbicacionPiezas, Title="Gestionar Control de Ubicacion de Piezas"},
                new HomeMenuItem {Id = MenuItemType.OrdenesClientes, Title="Gestionar Ordenes de Clientes"},
                new HomeMenuItem {Id = MenuItemType.OperacionesCalzados, Title="Gestionar Operaciones de Calzados"},
                new HomeMenuItem {Id = MenuItemType.Almacen, Title="Almacen"},
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}