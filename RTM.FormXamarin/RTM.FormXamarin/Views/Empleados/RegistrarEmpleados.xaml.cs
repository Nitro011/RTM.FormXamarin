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
    public partial class RegistrarEmpleados : ContentPage
    {
        EmpleadosViewModel Empleados;
        public RegistrarEmpleados()
        {
            InitializeComponent();
            BindingContext = this.Empleados = new EmpleadosViewModel();
        }

        async void ObtenerRoles(object sender, EventArgs e)
        {
            var pickerRol = pickerRoles.SelectedIndex;

            await DisplayAlert("Mostrar Roles", pickerRol.ToString(), "Aceptar");
        }
    }
}