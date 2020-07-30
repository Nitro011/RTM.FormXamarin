using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RTM.FormXamarin.Views.GestionOrdenesPO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionPO : ContentPage
    {
        GestionPOViewModel GestionPOViewModel;
        public GestionPO()
        {
            InitializeComponent();
            BindingContext = this.GestionPOViewModel=new GestionPOViewModel();
            abrirRegistroClientes.Clicked += AbrirRegistroClientes_Clicked;
            abrirRegistroOrdenesClientes.Clicked += AbrirRegistroOrdenesClientes_Clicked;
        }

        private async void AbrirRegistroOrdenesClientes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdenesClientes.GestionarOrdenesClientes());
        }

        private async void AbrirRegistroClientes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clientes.GestionarClientes());
        }
    }
}