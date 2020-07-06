using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.Clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarClientes : ContentPage
    {
        GestionarClientesViewModel GestionarClientesViewModel;
        public GestionarClientes()
        {
            InitializeComponent();
            BindingContext = this.GestionarClientesViewModel = new GestionarClientesViewModel();
            abrirRegistroDeClientes.Clicked += AbrirRegistroDeClientes_Clicked;
            abrirConsultasDeClientes.Clicked += AbrirConsultasDeClientes_Clicked;
           
        }

        private async void AbrirConsultasDeClientes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clientes.ConsultarClientes());
        }

        private async void AbrirRegistroDeClientes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clientes.RegistrarClientes());
        }
    }
}