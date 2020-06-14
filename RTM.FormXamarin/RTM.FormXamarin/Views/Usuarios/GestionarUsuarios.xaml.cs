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
        public GestionarUsuarios()
        {
            InitializeComponent();
        }

        private async void abrirRegistrarUsuarios(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios.RegistrarUsuarios());
        }
    }
}