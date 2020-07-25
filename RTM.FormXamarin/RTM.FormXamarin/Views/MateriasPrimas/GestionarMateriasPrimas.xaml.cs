using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

            agregarNuevasMateriasPrimas.Clicked += AgregarNuevasMateriasPrimas_Clicked;
        }

        private async void AgregarNuevasMateriasPrimas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MateriasPrimas.RegistrarMateriasPrimas());
        }
    }
}