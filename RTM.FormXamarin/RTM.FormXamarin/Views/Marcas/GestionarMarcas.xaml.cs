using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Marcas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarMarcas : ContentPage
    {
        GestionarMarcasViewModel GestionarMarcasViewModel = new GestionarMarcasViewModel();
        public GestionarMarcas()
        {
            InitializeComponent();
            BindingContext = this.GestionarMarcasViewModel = new GestionarMarcasViewModel();
            abrirRegistroDeMarcas.Clicked += AbrirRegistroDeMarcas_Clicked;
            abrirConsultasDeMarcas.Clicked += AbrirConsultasDeMarcas_Clicked;
        }

        private async void AbrirConsultasDeMarcas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Marcas.ConsultarMarcas());
        }

        private async void AbrirRegistroDeMarcas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Marcas.RegistrarMarcas());
        }
    }
}