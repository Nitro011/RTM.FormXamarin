using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RTM.FormXamarin.Models;
using RTM.FormXamarin.Views.Usuarios;
using RTM.FormXamarin.Views.Roles;
using RTM.FormXamarin.Views.Empleados;
using RTM.FormXamarin.Views.MateriasPrimas;
using RTM.FormXamarin.Views.TIposMateriales;
using RTM.FormXamarin.Models.Marcas;

namespace RTM.FormXamarin.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            MenuPages.Add((int)MenuItemType.PaginaPrincipal, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.PaginaPrincipal:
                        MenuPages.Add(id, new NavigationPage(new Master()));
                        break;
                    case (int)MenuItemType.GestionHumana:
                        MenuPages.Add(id, new NavigationPage(new GestionHumana.GestionHumana()));
                        break;
                    case (int)MenuItemType.Ingenieria:
                        MenuPages.Add(id, new NavigationPage(new Ingenieria.Ingenieria()));
                        break;
                    case (int)MenuItemType.GestionEstilo:
                        MenuPages.Add(id, new NavigationPage(new GestionEstilos.GestionEstilos()));
                        break;
                    case (int)MenuItemType.GestionMateriales:
                        MenuPages.Add(id, new NavigationPage(new GestionMateriales.GestionMateriales()));
                        break;
                    case (int)MenuItemType.Configuraciones:
                        MenuPages.Add(id, new NavigationPage(new Configuraciones.Configuraciones()));
                        break;
                    case (int)MenuItemType.GestionarControlUbicacionPiezas:
                        MenuPages.Add(id, new NavigationPage(new ControlUbicacionPiezas.GestionarControlUbicacionPiezas()));
                        break;
                    case (int)MenuItemType.OrdenesClientes:
                        MenuPages.Add(id, new NavigationPage(new OrdenesClientes.GestionarOrdenesClientes()));
                        break;
                    case (int)MenuItemType.OperacionesCalzados:
                        MenuPages.Add(id, new NavigationPage(new OperacionesCalzados.GestionarOperacionesCalzados()));
                        break;
                    case (int)MenuItemType.Almacen:
                        MenuPages.Add(id, new NavigationPage(new Almacen.GestionarAlmacen()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                  
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
