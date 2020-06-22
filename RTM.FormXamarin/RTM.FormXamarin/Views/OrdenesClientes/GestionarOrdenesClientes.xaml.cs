using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.OrdenesClientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarOrdenesClientes : ContentPage
    {
        public GestionarOrdenesClientes()
        {
            InitializeComponent();
            abrirRegistroDeOrdenesClientes.Clicked += AbrirRegistroDeOrdenesClientes_Clicked;
            abrirRegistroDeOrdenesClientesDetalles.Clicked += AbrirRegistroDeOrdenesClientesDetalles_Clicked;
        }

        private async void AbrirRegistroDeOrdenesClientesDetalles_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdenesClientes.RegistrarOrdenesClientesDetalles());
        }

        private async void AbrirRegistroDeOrdenesClientes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdenesClientes.RegistrarOrdenesClientes());
        }
    }
}