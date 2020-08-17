using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.GestionProduccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarProduccion : ContentPage
    {
        public GestionarProduccion()
        {
            InitializeComponent();
            abrirListadoDeProduccion.Clicked += AbrirListadoDeProduccion_Clicked;
        }

        private async void AbrirListadoDeProduccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GestionProduccion.ListadoOperacionesCalzados());
        }
    }
}