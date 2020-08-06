using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Estilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarEstilos : ContentPage
    {
        public GestionarEstilos()
        {
            InitializeComponent();
            agregarNuevoEstilo.Clicked += AgregarNuevoEstilo_Clicked;
        }

        private async void AgregarNuevoEstilo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Estilos.RegistrarEstilos());
        }
    }
}