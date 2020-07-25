using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Dimensiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDimensiones : ContentPage
    {
        GestionarDimensionesViewModel GestionarDimensionesViewModel;
        public GestionarDimensiones()
        {
            InitializeComponent();
            BindingContext = this.GestionarDimensionesViewModel = new GestionarDimensionesViewModel();
            agregarNuevosSizes.Clicked += AgregarNuevosSizes_Clicked;
        }

        private async void AgregarNuevosSizes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Dimensiones.RegistrarDimensiones());
        }
    }
}