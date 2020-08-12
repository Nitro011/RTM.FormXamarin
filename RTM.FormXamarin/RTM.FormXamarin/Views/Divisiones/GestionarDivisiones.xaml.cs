using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Divisiones;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Divisiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDivisiones : ContentPage
    {
        public GestionarDivisiones()
        {
            InitializeComponent();
            ListaDivisiones();
        }

        private async void ListaDivisiones()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Divisiones/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<DivisionesListView>>(response.data.ToString());

                    listaDivisiones.ItemsSource = listaView;


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