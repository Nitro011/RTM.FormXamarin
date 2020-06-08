using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarEmpleadosYUsuarios : ContentPage
    {
        GestionarEmpleadosYUsuariosViewModel EmpleadosYUsuarios;
        public GestionarEmpleadosYUsuarios()
        {
            InitializeComponent();
            BindingContext = this.EmpleadosYUsuarios = new GestionarEmpleadosYUsuariosViewModel();
        }

        private async void abrirRegistrarEmpleadosYUsuarios(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.RegistrarEmpleados());
        }

        private async void abrirConsultarEmpleados(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.ConsultarEmpleados());
        }

        private async void abrirModificarEmpleados(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.ConsultarEmpleadosParaModificar());
        }
    }
}