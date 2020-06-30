using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.BOM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarBOM : ContentPage
    {
        public GestionarBOM()
        {
            InitializeComponent();
            abrirRegistroDeBOM.Clicked += AbrirRegistroDeBOM_Clicked;
        }

        private async void AbrirRegistroDeBOM_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BOM.RegistrarBOM());
        }
    }
}