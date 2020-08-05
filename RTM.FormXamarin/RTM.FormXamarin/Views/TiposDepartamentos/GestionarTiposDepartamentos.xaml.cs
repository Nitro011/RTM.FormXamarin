using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.TiposDepartamentos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.TiposDepartamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarTiposDepartamentos : ContentPage
    {
        public GestionarTiposDepartamentos()
        {
            InitializeComponent();

            ListaTiposDepartamentos();

            buscarTiposDepartamentos.TextChanged += BuscarTiposDepartamentos_TextChanged;
        }

        private void BuscarTiposDepartamentos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarTiposDepartamentos.Text == "")
            {
                ListaTiposDepartamentos();
            }
            else
            {
                string TipoDepartamento = buscarTiposDepartamentos.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/TiposDepartamentos/ConsultarTiposDepartamentosPorTipoDepartamento/{TipoDepartamento}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<TiposDepartamentosListView>>(response.data.ToString());

                            listaTiposDepartamentos.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void ListaTiposDepartamentos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/TiposDepartamentos/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<TiposDepartamentosListView>>(response.data.ToString());

                    listaTiposDepartamentos.ItemsSource = listaView;


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