using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.TIposMateriales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarTipoMaterial : ContentPage
    {
        GestionarTiposMaterialesViewModel GestionarTiposMaterialesViewModel;
        public GestionarTipoMaterial()
        {
            InitializeComponent();
            BindingContext = this.GestionarTiposMaterialesViewModel = new GestionarTiposMaterialesViewModel();
            abrirRegistroDeTiposMateriales.Clicked += AbrirRegistroDeTiposMateriales_Clicked;
            abrirConsultaDeTiposMateriales.Clicked += AbrirConsultaDeTiposMateriales_Clicked;
        }

        private async void AbrirConsultaDeTiposMateriales_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TIposMateriales.ConsultarTiposMateriales());
        }

        private async void AbrirRegistroDeTiposMateriales_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TIposMateriales.RegistrarTiposMateriales());
        }
    }
}