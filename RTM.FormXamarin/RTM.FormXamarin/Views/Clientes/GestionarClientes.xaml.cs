using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Clientes;
using RTM.FormXamarin.ViewModels;
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
            ListaClientes();
            listaClientes.ItemSelected += ListaClientes_ItemSelected;
            agregarNuevoCliente.Clicked += AgregarNuevoCliente_Clicked;
            buscarCliente.TextChanged += BuscarCliente_TextChanged;
           
        }

        private void BuscarCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarCliente.Text == "")
            {
                ListaClientes();
            }
            else
            {
                string CodigoCliente = buscarCliente.Text;
                string RNC = buscarCliente.Text;
                string NombreCliente = buscarCliente.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Clientes/ConsultarClientesPorCodigoClienteRNCNombreCliente/{CodigoCliente}/{RNC}/{NombreCliente}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<ClientesListView>>(response.data.ToString());

                            listaClientes.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void AgregarNuevoCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clientes.RegistrarClientes());
        }

        private void ListaClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (ClientesListView)e.SelectedItem;

                Navigation.PushAsync(new InformacionCliente(item.ClienteID));
            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async void ListaClientes()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Clientes/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ClientesListView>>(response.data.ToString());

                    listaClientes.ItemsSource = listaView;


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