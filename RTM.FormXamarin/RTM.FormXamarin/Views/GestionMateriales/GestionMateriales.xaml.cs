using RTM.FormXamarin.ViewModels;
using RTM.FormXamarin.Views.Suplidores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.GestionMateriales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionMateriales : ContentPage
    {
        GestionMaterialesViewModel GestionMaterialesViewModel;
        public GestionMateriales()
        {
            InitializeComponent();
            BindingContext = this.GestionMaterialesViewModel=new GestionMaterialesViewModel();
            abrirGestionSuplidores.Clicked += AbrirGestionSuplidores_Clicked;
            abrirGestionMateriales.Clicked += AbrirGestionMateriales_Clicked;
        }

        private async void AbrirGestionMateriales_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MateriasPrimas.GestionarMateriasPrimas());
        }

        private async void AbrirGestionSuplidores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Suplidores.GestionarSuplidores());
        }
    }
}