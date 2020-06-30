using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.OperacionesCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarOperacionesCalzados : ContentPage
    {
        public GestionarOperacionesCalzados()
        {
            InitializeComponent();
            abrirRegistroDeOperacionesDeCalzados.Clicked += AbrirRegistroDeOperacionesDeCalzados_Clicked;
        }

        private async void AbrirRegistroDeOperacionesDeCalzados_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OperacionesCalzados.RegistrarOperacionesCalzados());
        }
    }
}