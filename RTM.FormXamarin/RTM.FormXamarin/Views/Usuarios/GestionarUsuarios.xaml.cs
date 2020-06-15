using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarUsuarios : ContentPage
    {
        GestionarUsuariosViewModel Usuarios;
        public GestionarUsuarios()
        {
            InitializeComponent();
            BindingContext = this.Usuarios = new GestionarUsuariosViewModel();
            abrirRegistrarUsuarios.Clicked += AbrirRegistrarUsuarios_Clicked;
            abrirConsultarUsuarios.Clicked += AbrirConsultarUsuarios_Clicked;
            abrirModificarUsuarios.Clicked += AbrirModificarUsuarios_Clicked; 
        }

        private async void AbrirModificarUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios.ConsultarUsuariosParaModificar());
        }

        private async void AbrirConsultarUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios.ConsultarUsuarios());
        }

        private async void AbrirRegistrarUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios.RegistrarUsuarios());
        }
    }
}