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
            agregarNuevaOrdenCliente.Clicked += AgregarNuevaOrdenCliente_Clicked;
        }

        private async void AgregarNuevaOrdenCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdenesClientes.RegistrarOrdenesClientes());
        }
    }
}