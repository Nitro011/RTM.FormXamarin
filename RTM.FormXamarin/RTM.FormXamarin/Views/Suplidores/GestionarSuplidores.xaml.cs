using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Suplidores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarSuplidores : ContentPage
    {
        GestionarSuplidoresViewModel GestionarSuplidoresViewModel;
        public GestionarSuplidores()
        {
            InitializeComponent();
            BindingContext = this.GestionarSuplidoresViewModel = new GestionarSuplidoresViewModel();
            abrirRegistrarSuplidores.Clicked += AbrirRegistrarSuplidores_Clicked;
            abrirConsultarSuplidores.Clicked += AbrirConsultarSuplidores_Clicked;
            abrirModificarSuplidores.Clicked += AbrirModificarSuplidores_Clicked;
        }

        private async void AbrirModificarSuplidores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Suplidores.ConsultarSuplidoresParaModificar());
        }

        private async void AbrirConsultarSuplidores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Suplidores.ConsultarSuplidores());
        }

        private async void AbrirRegistrarSuplidores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Suplidores.RegistrarSuplidores());
        }
    }
}