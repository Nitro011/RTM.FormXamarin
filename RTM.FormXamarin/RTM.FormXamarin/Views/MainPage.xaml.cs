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

            MenuPages.Add((int)MenuItemType.master, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.master:
                        MenuPages.Add(id, new NavigationPage(new Master()));
                        break;
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.Configuraciones:
                        MenuPages.Add(id, new NavigationPage(new Configuraciones.Configuraciones()));
                        break;
                    case (int)MenuItemType.TiposCalzados:
                        MenuPages.Add(id, new NavigationPage(new TiposCalzados.GestionarTiposCalzados()));
                        break;
                    case (int)MenuItemType.GestionarModelos:
                        MenuPages.Add(id, new NavigationPage(new Modelos.GestionarModelos()));
                        break;
                    case (int)MenuItemType.Clientes:
                        MenuPages.Add(id, new NavigationPage(new Clientes.RegistrarClientes()));
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
