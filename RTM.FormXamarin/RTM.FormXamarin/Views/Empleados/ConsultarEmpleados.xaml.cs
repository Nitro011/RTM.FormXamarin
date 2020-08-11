using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Empleados;
using RTM.FormXamarin.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarEmpleados : ContentPage
    {
        public ConsultarEmpleados()
        {
            InitializeComponent();
            ListaEmpleado();
            listaEmpleado.ItemSelected += ListaEmpleado_ItemSelected;
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

                        var listaView = JsonConvert.DeserializeObject<List<EmpleadoListView>>(response.data.ToString());

                        /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                        listaEmpleado.ItemsSource = listaView;
                    }

                }
            }
        }


        private void ListaEmpleado_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (EmpleadoListView)e.SelectedItem;

                Navigation.PushAsync(new InformacionDelEmpleado(item.Id));
            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Aceptar");
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