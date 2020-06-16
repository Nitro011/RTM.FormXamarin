﻿using RTM.FormXamarin.Models;
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
                new HomeMenuItem {Id = MenuItemType.master, Title="Master" },
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                new HomeMenuItem {Id = MenuItemType.Empleados, Title="Gestionar Empleados"},
                new HomeMenuItem {Id = MenuItemType.Usuarios, Title="Gestionar Usuarios"},
                new HomeMenuItem {Id = MenuItemType.Suplidores, Title="Gestionar Suplidores"},
                new HomeMenuItem {Id = MenuItemType.GestionarAreasDeProduccion, Title="Gestionar Areas de Produccion"},
                new HomeMenuItem {Id = MenuItemType.GestionarMateriasPrimas, Title="Gestionar Materias Primas"},
                new HomeMenuItem {Id = MenuItemType.RegistrarRoles, Title="Roles"},
                new HomeMenuItem {Id = MenuItemType.Clientes, Title="Clientes" },
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