using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.TiposCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarTiposCalzados : ContentPage
    {
        TiposCalzadoViewModel TiposCalzadoViewModel;
        public GestionarTiposCalzados()
        {
            InitializeComponent();
            BindingContext = this.TiposCalzadoViewModel = new TiposCalzadoViewModel();
            abrirRegistroDeTipoDeCalzado.Clicked += AbrirRegistroDeTipoDeCalzado_Clicked;
            abrirConsultasDeTipoDeCalzado.Clicked += AbrirConsultasDeTipoDeCalzado_Clicked;
            abrirTab.Clicked += AbrirTab_Clicked;
        }

        private async void AbrirTab_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TiposCalzados.ejemplo());
        }

        private async void AbrirConsultasDeTipoDeCalzado_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new TiposCalzados.ConsultarTiposCalzados());
        }

        private async void AbrirRegistroDeTipoDeCalzado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TiposCalzados.RegistrarTipoDeCalzado());
        }
    }
}