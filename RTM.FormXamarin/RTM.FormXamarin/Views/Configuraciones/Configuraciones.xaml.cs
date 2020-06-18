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
using Android.Util;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms;
using Android.App;

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
            abrirRegistroDeEmpleados.Clicked += AbrirRegistroDeEmpleados_Clicked;
            abrirRegistroDeUsuarios.Clicked += AbrirRegistroDeUsuarios_Clicked;
            abrirRegistroDeRoles.Clicked += AbrirRegistroDeRoles_Clicked;
            abrirRegistroDeAreasDeProduccion.Clicked += AbrirRegistroDeAreasDeProduccion_Clicked;
            abrirConsultasDeEmpleados.Clicked += AbrirConsultasDeEmpleados_Clicked;
            abrirConsultasDeUsuarios.Clicked += AbrirConsultasDeUsuarios_Clicked;
            abrirConsultasDeRoles.Clicked += AbrirConsultasDeRoles_Clicked;
            abrirConsultasDeAreasDeProduccion.Clicked += AbrirConsultasDeAreasDeProduccion_Clicked;
        }

        private async void AbrirConsultasDeAreasDeProduccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AreaDeProduccion.ConsultarAreaProduccion());
        }

        private async void AbrirConsultasDeRoles_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Comming Soon", "Ya pronto funcionara", "Aceptar");
        }

        private async void AbrirConsultasDeUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios.ConsultarUsuarios());
        }

        private async void AbrirConsultasDeEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.ConsultarEmpleados());
        }

        private async void AbrirRegistroDeRoles_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Roles.RegistrarRoles());
        }

        private async void AbrirRegistroDeUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios.RegistrarUsuarios());
        }

        private async void AbrirRegistroDeEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.RegistrarEmpleados());
        }

        private async void AbrirRegistroDeAreasDeProduccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AreaDeProduccion.RegistrarAreaDeProduccion());
        }
    }
}