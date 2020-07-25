using RTM.FormXamarin.Models.Modelos;
using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.GestionEstilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionEstilos : ContentPage
    {
        GestionarEstilosViewModel GestionarEstilosViewModel;
        public GestionEstilos()
        {
            InitializeComponent();
            BindingContext = this.GestionarEstilosViewModel=new GestionarEstilosViewModel();
            abrirGestionMarcas.Clicked += AbrirGestionMarcas_Clicked;
            abrirGestionTiposEstilos.Clicked += AbrirGestionTiposEstilos_Clicked;
            abrirGestionModelos.Clicked += AbrirGestionModelos_Clicked;
            abrirGestionSize.Clicked += AbrirGestionSize_Clicked;
            abrirGestionColores.Clicked += AbrirGestionColores_Clicked;

        }

        private async void AbrirGestionColores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Colores.GestionarColores());
        }

        private async void AbrirGestionSize_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Dimensiones.GestionarDimensiones());
        }

        private async void AbrirGestionModelos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Modelos.GestionarModelos());
        }

        private async void AbrirGestionTiposEstilos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TiposCalzados.GestionarTiposCalzados());
        }

        private async void AbrirGestionMarcas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Marcas.GestionarMarcas());
        }
    }
}