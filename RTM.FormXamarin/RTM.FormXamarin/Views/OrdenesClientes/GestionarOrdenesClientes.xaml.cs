using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models.OrdenesClientes;
using RTM.FormXamarin.ViewModels;

namespace RTM.FormXamarin.Views.OrdenesClientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarOrdenesClientes : ContentPage
    {
        public GestionarOrdenesClientes()
        {
            InitializeComponent();
            agregarNuevaOrdenCliente.Clicked += AgregarNuevaOrdenCliente_Clicked;
            ListaOrdenesClientes();
        }

        private async void AgregarNuevaOrdenCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdenesClientes.RegistrarOrdenesClientes());
        }

        private async void ListaOrdenesClientes()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/OrdenesClientes/OrdenesClientesList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<OrdenesClientesListView>>(response.data.ToString());

                    listaOrdenesClientes.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }
    }
}