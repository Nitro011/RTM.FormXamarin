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
using RTM.FormXamarin.Models.Usuarios;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarEmpleados : ContentPage
    {
        public GestionarEmpleados()
        {
            InitializeComponent();
            ListaEmpleado();
            buscarEmpleado.TextChanged += BuscarEmpleado_TextChanged;
            agregarNuevoEmpleado.Clicked += AgregarNuevoEmpleado_Clicked;
        }

        private async void AgregarNuevoEmpleado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.RegistrarEmpleados());
        }

        private void BuscarEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarEmpleado.Text == "")
            {
                ListaEmpleado();
            }
            else
            {
                string Nombres = buscarEmpleado.Text;
                string Apellidos = buscarEmpleado.Text;
                string Cedula = buscarEmpleado.Text;
                string CodigoEmpleado = buscarEmpleado.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Empleados/EmpleadoPorNombresApellidosCedulaCodigoEmpleado/{Nombres}/{Apellidos}/{Cedula}/{CodigoEmpleado}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<EmpleadoListView>>(response.data.ToString());

                            /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                            listaEmpleado.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void ListaEmpleado()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Empleados/EmpleadosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<EmpleadoListView>>(response.data.ToString());

                    listaEmpleado.ItemsSource = listaView;


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