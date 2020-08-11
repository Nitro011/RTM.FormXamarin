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
            abrirGestionDeEmpleados.Clicked += AbrirGestionDeEmpleados_Clicked;
            abrirGestionDeUsuarios.Clicked += AbrirGestionDeUsuarios_Clicked;
            abrirGestionDeAreasDeProduccion.Clicked += AbrirGestionDeAreasDeProduccion_Clicked;
            abrirGestionDeColores.Clicked += AbrirGestionDeColores_Clicked;
            abrirGestionDeOperacionesCalzados.Clicked += AbrirGestionDeOperacionesCalzados_Clicked;
        }

        private async void AbrirGestionDeOperacionesCalzados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OperacionesCalzados.GestionarOperacionesCalzados());
        }

        private async void AbrirGestionDeColores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Colores.GestionarColores());
        }

        private async void AbrirGestionDeAreasDeProduccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AreaDeProduccion.GestionarAreaDeProduccion());
        }

        private async void AbrirGestionDeUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios.GestionarUsuarios());
        }

        private async void AbrirGestionDeEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.GestionarEmpleadosYUsuarios());
        }


    }
}