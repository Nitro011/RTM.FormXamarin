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
            buscarEstilo.TextChanged += BuscarEstilo_TextChanged;
            listaEstilos.ItemSelected += ListaEstilos_ItemSelected;
        }

        private async void ListaEstilos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Mostrar Informacion?", "Desea ver la informacion general de este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (EstilosListView)e.SelectedItem;

                    await Navigation.PushAsync(new InformacionDetalleEstilo(item.EstiloID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaEstilos();
            }
        }

        private void BuscarEstilo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarEstilo.Text == "")
            {
                ListaEstilos();
            }
            else
            {
                string EstiloNo = buscarEstilo.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/EstilosNuevos/ConsultarEstilosPorEstiloNo/{EstiloNo}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<EstilosListView>>(response.data.ToString());

                            listaEstilos.ItemsSource = listaView;
                        }
                    }

                }
            }
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