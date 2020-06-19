using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Almacen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarAlmacen : ContentPage
    {
        GestionarAlmacenViewModel GestionarAlmacenViewModel;
        public GestionarAlmacen()
        {
            InitializeComponent();
            BindingContext = this.GestionarAlmacenViewModel = new GestionarAlmacenViewModel();
            abrirRegistroDeAlmacen.Clicked += AbrirRegistroDeAlmacen_Clicked;
            abrirRegistroDeMateriasPrimas.Clicked += AbrirRegistroDeMateriasPrimas_Clicked;
            abrirRegistroDeTiposDeMateriales.Clicked += AbrirRegistroDeTiposDeMateriales_Clicked;
            abrirRegistroDeSuplidores.Clicked += AbrirRegistroDeSuplidores_Clicked;
            //abrirConsultasDeAlmacen.Clicked += AbrirConsultasDeAlmacen_Clicked;
            //abrirConsultasDeMateriasPrimas.Clicked += AbrirConsultasDeMateriasPrimas_Clicked;
            abrirConsultasDeTiposDeMateriales.Clicked += AbrirConsultasDeTiposDeMateriales_Clicked;
            abrirConsultasDeSuplidores.Clicked += AbrirConsultasDeSuplidores_Clicked;
        }

        private async void AbrirConsultasDeSuplidores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Suplidores.ConsultarSuplidores());
        }

        private async void AbrirConsultasDeTiposDeMateriales_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TIposMateriales.ConsultarTiposMateriales());
        }

        //private async void AbrirConsultasDeMateriasPrimas_Clicked(object sender, EventArgs e)
        //{
           
        //}

        //private async void AbrirConsultasDeAlmacen_Clicked(object sender, EventArgs e)
        //{
            
        //}

        private async void AbrirRegistroDeSuplidores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Suplidores.RegistrarSuplidores());
        }

        private async void AbrirRegistroDeTiposDeMateriales_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TIposMateriales.RegistrarTiposMateriales());
        }

        private async void AbrirRegistroDeMateriasPrimas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MateriasPrimas.RegistrarMateriasPrimas());
        }

        private async void AbrirRegistroDeAlmacen_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Almacen.RegistrarAlmacen());
        }
    }
}