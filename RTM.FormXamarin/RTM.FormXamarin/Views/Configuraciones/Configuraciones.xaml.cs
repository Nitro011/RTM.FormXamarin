using RTM.FormXamarin.Models.AreasDeProduccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Views.AreaDeProduccion;
using RTM.FormXamarin.Views.Empleados;
using RTM.FormXamarin.Views.Usuarios;
using RTM.FormXamarin.Views.Roles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RTM.FormXamarin.Models.Usuarios;
using RTM.FormXamarin.ViewModels;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms;
using RTM.FormXamarin.Models.ITEMS;

namespace RTM.FormXamarin.Views.Configuraciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuraciones : ContentPage
    {
        ConfiguracionesViewModel ConfiguracionesViewModel;
        public Configuraciones()
        {
            InitializeComponent();
            BindingContext = this.ConfiguracionesViewModel = new ConfiguracionesViewModel();
            abrirGestionDeItems.Clicked += AbrirGestionDeItems_Clicked;
        }

        private async void AbrirGestionDeItems_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ITEMS.GestionarItems());
        }
    }
}