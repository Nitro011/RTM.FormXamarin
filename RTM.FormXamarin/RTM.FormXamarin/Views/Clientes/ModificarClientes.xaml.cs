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
using RTM.FormXamarin.Models.Clientes;

namespace RTM.FormXamarin.Views.Clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarClientes : ContentPage
    {
        public int clienteID;
        public ModificarClientes(int ClienteID)
        {
            InitializeComponent();
            mostrarInformacionClientes(ClienteID);
        }

        private void mostrarInformacionClientes(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Clientes/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var listaView = JsonConvert.DeserializeObject<ClientesListView>(response.data.ToString());
                    clienteID = listaView.ClienteID;
                    codigoCliente.Text = listaView.CodigoCliente;
                    RNC.Text = listaView.RNC;
                    nombreCliente.Text = listaView.Nombre_Cliente;
                    correoElectronico.Text = listaView.Correo_Electronico;
                    noTelefono.Text = listaView.No_Telefono;
                    pais.Text = listaView.Pais;
                    direccion.Text = listaView.Direccion;
                }

            }
        }
    }
}