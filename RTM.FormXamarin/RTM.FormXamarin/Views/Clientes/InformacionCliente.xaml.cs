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
    public partial class InformacionCliente : ContentPage
    {
        public InformacionCliente(int id)
        {
            InitializeComponent();
            MostrarInformacionCliente(id);
        }

        private void MostrarInformacionCliente(int id)
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

                    /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                    ClienteID.Text = listaView.ClienteID.ToString();
                    CodigoCliente.Text = listaView.CodigoCliente;
                    RNC.Text = listaView.RNC;
                    Nombre_Cliente.Text = listaView.Nombre_Cliente;
                    Correo_Electronico.Text = listaView.Correo_Electronico;
                    No_Telefono.Text = listaView.No_Telefono;
                    Pais.Text = listaView.Pais;
                    Direccion.Text = listaView.Direccion;
                }

            }

        }
    }
}