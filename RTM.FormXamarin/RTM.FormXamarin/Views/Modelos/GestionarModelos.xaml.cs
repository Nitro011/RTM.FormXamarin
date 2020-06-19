using RTM.FormXamarin.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Modelos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarModelos : ContentPage
    {
        public GestionarModelos()
        {
            InitializeComponent();
            abrirRegistroDeModelos.Clicked += AbrirRegistroDeModelos_Clicked;
            abrirConsultaDeModelos.Clicked += AbrirConsultaDeModelos_Clicked;
        }

        private async void AbrirConsultaDeModelos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Modelos.ConsultarModelos()); 
        }

        private async void AbrirRegistroDeModelos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Modelos.RegistrarModelos());
        }
    }
}