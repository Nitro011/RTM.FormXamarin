using Org.Apache.Http.Client.Methods;
using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.AreaDeProduccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarAreaDeProduccion : ContentPage
    {
        GestionarAreasDeProduccionViewModel gestionarAreasDeProduccion;
        public GestionarAreaDeProduccion()
        {
            InitializeComponent();
            BindingContext = this.gestionarAreasDeProduccion = new GestionarAreasDeProduccionViewModel();
            abrirRegistroDeAreasProduccion.Clicked += AbrirRegistroDeAreasProduccion_Clicked;
            abrirConsultaDeAreasProduccion.Clicked += AbrirConsultaDeAreasProduccion_Clicked;
        }

        private async void AbrirConsultaDeAreasProduccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AreaDeProduccion.ConsultarAreaProduccion());
        }

        private async void AbrirRegistroDeAreasProduccion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AreaDeProduccion.RegistrarAreaDeProduccion());
        }
    }
}