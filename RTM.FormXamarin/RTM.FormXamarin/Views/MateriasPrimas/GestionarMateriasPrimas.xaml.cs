using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.MateriasPrimas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarMateriasPrimas : ContentPage
    {
        GestionarMateriasPrimasViewModel GestionarMateriasPrimasViewModel;
        public GestionarMateriasPrimas()
        {
            InitializeComponent();
            BindingContext = this.GestionarMateriasPrimasViewModel = new GestionarMateriasPrimasViewModel();

            abrirRegistroDeMateriasPrimas.Clicked += AbrirRegistroDeMateriasPrimas_Clicked;
        }

        private async void AbrirRegistroDeMateriasPrimas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MateriasPrimas.RegistrarMateriasPrimas());
        }
    }
}