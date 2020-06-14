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
        }

        private async void abrirRegistroDeAreasDeProduccion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AreaDeProduccion.RegistrarAreaDeProduccion());
        }
    }
}