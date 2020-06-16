using Newtonsoft.Json;
using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PCLAppConfig;
using RTM.FormXamarin.Models.Empleados;

namespace RTM.FormXamarin.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarUsuarios : ContentPage
    {
        RegistrarUsuariosViewModel Usuarios;
   
        public RegistrarUsuarios()
        {
            InitializeComponent();
            BindingContext = this.Usuarios = new RegistrarUsuariosViewModel();
            cedula.TextChanged += Cedula_TextChanged;
        }

        private void Cedula_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Cedula = cedula.Text;
        }

        async void ObtenerRoles(object sender, EventArgs e)
        {
            var pickerRol = pickerRoles.SelectedIndex;

           await DisplayAlert("Mostrar Roles", pickerRol.ToString(), "Aceptar");
        }

        
    }
}