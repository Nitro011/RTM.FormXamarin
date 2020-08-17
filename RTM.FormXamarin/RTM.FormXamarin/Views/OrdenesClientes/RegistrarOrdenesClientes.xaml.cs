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
using RTM.FormXamarin.Models.Marcas;
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models.Colores;
using RTM.FormXamarin.Models.Modelos;
using RTM.FormXamarin.Models.TiposCalzados;
using RTM.FormXamarin.Models.Sizes;
using RTM.FormXamarin.Models.MateriasPrimas;
using RTM.FormXamarin.Models.Estilos;
using RTM.FormXamarin.Models.OrdenesClientesSizes;
using RTM.FormXamarin.Models.OrdenesClientesEstilos;
using RTM.FormXamarin.Models.OrdenesClientes;
using RTM.FormXamarin.Models.Clientes;

namespace RTM.FormXamarin.Views.OrdenesClientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarOrdenesClientes : ContentPage
    {
        public int clienteID;
        public RegistrarOrdenesClientes()
        {
            InitializeComponent();
            ListaEstilos();
            ListaSize();
            btnGuardarOrdenesClientes.Clicked += BtnGuardarOrdenesClientes_Clicked;
            buscarCliente.TextChanged += BuscarCliente_TextChanged;
        }

        private void BuscarCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            string CodigoCliente = buscarCliente.Text;
            string RNC = buscarCliente.Text;
            string NombreCliente = buscarCliente.Text;

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Clientes/ConsultarClientePorCodigoClienteRNCNombreCliente/{CodigoCliente}/{RNC}/{NombreCliente}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    if (response.data != null)
                    {

                        var listaView = JsonConvert.DeserializeObject<ClientesListView>(response.data.ToString());

                        /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                        clienteID = listaView.ClienteID;
                        nombreCliente.Text = listaView.Nombre_Cliente;
                    }
                }
                else
                {
                    MaterialDialog.Instance.AlertAsync(message: "Error",
                                                       title: "Error",
                                                       acknowledgementText: "Aceptar");
                }

            }
        }

        private async void BtnGuardarOrdenesClientes_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            var ListaEstilos = new List<OrdenesClientes_Estilos>();
            var ListaSizes = new List<OrdenesClientes_Sizes>();



            try
            {

                var CodigoQRVar = codigoQR.Text;
                var ClienteIDVar = clienteID;
                var CantidadCalzadosRealizarVar = cantidadCalzadoRealizar.Text;
                var FechaInicioVar = fechaInicio.Date;
                var FechaEntregaVar = fechaEntrega.Date;

                var EstilosComboBoxVar = (IList<object>)estilosComboBox.SelectedItem;

                foreach (var item in EstilosComboBoxVar)
                {
                    var estiloID = (EstilosListView)item;
                    var EstilosLista = new OrdenesClientes_Estilos() { EstiloID = estiloID.EstiloID };
                    ListaEstilos.Add(EstilosLista);
                }

                var SizesComboBoxVar = (IList<object>)sizesComboBox.SelectedItem;

                foreach (var item in SizesComboBoxVar)
                {
                    var sizeID = (SizesListView)item;
                    var SizesLista = new OrdenesClientes_Sizes() { SizeID = sizeID.SizeID };
                    ListaSizes.Add(SizesLista);
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var ordenesClientes = new OrdenesCliente()
                {

                    CodigoQR = CodigoQRVar,
                    ClienteID = ClienteIDVar,
                    Cantidad_Calzado_Realizar = Convert.ToInt32(CantidadCalzadosRealizarVar),
                    Fecha_Inicio = DateTime.Parse(FechaInicioVar.ToString()),
                    Fecha_Entrega = DateTime.Parse(FechaEntregaVar.ToString()),
                    OrdenesClientes_Estilos = ListaEstilos,
                    OrdenesClientes_Sizes = ListaSizes

                };


                //Convetir a Json
                var json = JsonConvert.SerializeObject(ordenesClientes);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/OrdenesClientes/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Orden del Cliente se registro correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Orden del Cliente no se registro correctamente",
                                  title: "Registro",
                                  acknowledgementText: "Aceptar");

                    }

                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                    title: "Error",
                                    acknowledgementText: "Aceptar");
                }

            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message,
                                    title: "Error",
                                    acknowledgementText: "Aceptar");
            }
        }

        private async void ListaEstilos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/EstilosNuevos/EstilosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<EstilosListView>>(response.data.ToString());

                    estilosComboBox.DataSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaSize()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Size/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<SizesListView>>(response.data.ToString());

                    sizesComboBox.DataSource = listaView;


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