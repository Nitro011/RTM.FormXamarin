using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.AreasDeProduccion;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.AreaDeProduccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarAreaProduccion : ContentPage
    {
        public ConsultarAreaProduccion()
        {
            InitializeComponent();
            ListaAreaProduccion();
        }

        private async void ListaAreaProduccion()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/AreaProduccion/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<AreaProduccionListView>>(response.data.ToString());

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