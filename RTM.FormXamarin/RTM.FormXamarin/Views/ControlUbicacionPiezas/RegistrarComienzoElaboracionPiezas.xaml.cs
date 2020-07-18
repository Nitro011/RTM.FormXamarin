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
    public partial class RegistrarComienzoElaboracionPiezas : ContentPage
    {
        public RegistrarComienzoElaboracionPiezas()
        {
            InitializeComponent();
            abrirScanner.Clicked += AbrirScanner_Clicked;
        }

        private async void AbrirScanner_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FullScreenScannings.FullScreenScanning());
        }
    }
}