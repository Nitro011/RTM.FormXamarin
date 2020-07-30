using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.GestionUsuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarUsuarios : ContentPage
    {
        public GestionarUsuarios()
        {
            InitializeComponent();
            abrirDefiniciondeRoles.Clicked += AbrirDefiniciondeRoles_Clicked;
            abrirMenuUsuarios.Clicked += AbrirMenuUsuarios_Clicked;
        }

        private async void AbrirMenuUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GestionUsuarios.MenuUsuarios());
        }

        private async void AbrirDefiniciondeRoles_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Roles.GestionarRoles());
        }
    }
}