using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Colores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarColores : ContentPage
    {
        public GestionarColores()
        {
            InitializeComponent();
            abrirRegistroDeColores.Clicked += AbrirRegistroDeColores_Clicked;
            abrirConsultasDeColores.Clicked += AbrirConsultasDeColores_Clicked;
        }

        private async void AbrirConsultasDeColores_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Colores.ConsultarColores());
        }

        private async void AbrirRegistroDeColores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Colores.RegistrarColores());
        }
    }
}