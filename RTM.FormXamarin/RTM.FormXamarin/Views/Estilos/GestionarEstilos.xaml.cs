using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Estilos;
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

namespace RTM.FormXamarin.Views.Estilos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarEstilos : ContentPage
    {
        public GestionarEstilos()
        {
            InitializeComponent();
            ListaEstilos();
            agregarNuevoEstilo.Clicked += AgregarNuevoEstilo_Clicked;
        }

        private async void AgregarNuevoEstilo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Estilos.RegistrarEstilos());
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

                    listaEstilos.ItemsSource = listaView;


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