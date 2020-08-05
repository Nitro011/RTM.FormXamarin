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
using RTM.FormXamarin.Models.Departamentos;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Departamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDepartamento : ContentPage
    {
        public GestionarDepartamento()
        {
            InitializeComponent();
            ListaDepartamentos();
            buscarDepartamento.TextChanged += BuscarDepartamento_TextChanged;
        }

        private void BuscarDepartamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarDepartamento.Text == "")
            {
                ListaDepartamentos();
            }
            else
            {
                string Departamento = buscarDepartamento.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Departamento/BuscarDepartamentosPorDepartamento/{Departamento}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<DepartamentosListView>>(response.data.ToString());

                            /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                            listaAreaProduccion.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void ListaDepartamentos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Departamento/DepartamentosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<DepartamentosListView>>(response.data.ToString());

                    listaAreaProduccion.ItemsSource = listaView;


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