using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.ControlUbicacionPiezas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarControlUbicacionPiezas : ContentPage
    {
        public GestionarControlUbicacionPiezas()
        {
            InitializeComponent();
            abrirRegistroComienzoElaboracion.Clicked += AbrirRegistroComienzoElaboracion_Clicked;
        }

        private async void AbrirRegistroComienzoElaboracion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ControlUbicacionPiezas.RegistrarComienzoElaboracionPiezas());
        }
    }
}