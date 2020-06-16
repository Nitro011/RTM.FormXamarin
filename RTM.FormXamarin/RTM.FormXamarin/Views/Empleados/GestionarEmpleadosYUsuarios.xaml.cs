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
            abrirRegistrarEmpleados.Clicked += AbrirRegistrarEmpleados_Clicked;
            abrirConsultarEmpleados.Clicked += AbrirConsultarEmpleados_Clicked;
            abrirModificarEmpleados.Clicked += AbrirModificarEmpleados_Clicked;
        }

        private async void AbrirModificarEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.ConsultarEmpleadosParaModificar());
        }

        private async void AbrirConsultarEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.ConsultarEmpleados());
        }

        private  async void AbrirRegistrarEmpleados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.RegistrarEmpleados());
        }
    }
}