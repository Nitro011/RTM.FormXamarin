using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.GestionHumana
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionHumana : ContentPage
    {
        GestionHumanaViewModels GestionHumanaViewModels;
        public GestionHumana()
        {
            InitializeComponent();
            BindingContext = this.GestionHumanaViewModels = new GestionHumanaViewModels();
            abrirGestionPosiciones.Clicked += AbrirGestionPosiciones_Clicked;
            abrirGestionTiposDepartamentos.Clicked += AbrirGestionTiposDepartamentos_Clicked;
            abrirGestionDepartamentos.Clicked += AbrirGestionDepartamentos_Clicked;
            abrirGestionSubDepartamentos.Clicked += AbrirGestionSubDepartamentos_Clicked;
            abrirGestionEmpleados.Clicked += AbrirGestionEmpleados_Clicked;
        }

        private async void AbrirGestionEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.GestionarEmpleados());
        }

        private async void AbrirGestionSubDepartamentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubDepartamentos.GestionarSubDepartamentos());
        }

        private async void AbrirGestionDepartamentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Departamentos.GestionarDepartamento());
        }

        private async void AbrirGestionTiposDepartamentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TiposDepartamentos.GestionarTiposDepartamentos());
        }

        private async void AbrirGestionPosiciones_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Posiciones.GestionarPosiciones());
        }
    }
}