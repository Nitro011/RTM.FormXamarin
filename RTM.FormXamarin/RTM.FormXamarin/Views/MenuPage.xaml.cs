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
                new HomeMenuItem {Id = MenuItemType.PaginaPrincipal, Title="Página Principal" },
                new HomeMenuItem {Id = MenuItemType.GestionHumana, Title="Gestión Humana"},
                new HomeMenuItem {Id = MenuItemType.Ingenieria, Title="Ingeniería"},
                new HomeMenuItem {Id = MenuItemType.GestionEstilo, Title="Gestión de Estilos"},
                new HomeMenuItem {Id = MenuItemType.GestionOrdenesPO, Title="Gestión  de Ordenes PO"},
                new HomeMenuItem {Id = MenuItemType.GestionProduccion, Title="Gestión  de Producción"},
                new HomeMenuItem {Id = MenuItemType.GestionMateriales, Title="Gestión  de Materiales"},
                new HomeMenuItem {Id= MenuItemType.GestionUsuario, Title="Gestión  de Usuarios"},
                new HomeMenuItem {Id = MenuItemType.Configuraciones, Title="Configuraciones"},
                new HomeMenuItem {Id = MenuItemType.About, Title="Sobre Nosotros" }
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