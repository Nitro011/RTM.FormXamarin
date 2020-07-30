using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Modelos;
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

namespace RTM.FormXamarin.Views.Modelos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarModelos : ContentPage
    {
        public GestionarModelos()
        {
            InitializeComponent();
            agregarNuevosModelos.Clicked += AgregarNuevosModelos_Clicked;
            buscarModelos.TextChanged += BuscarModelos_TextChanged;
            listaModelos.ItemSelected += ListaModelos_ItemSelected;
            ListaModelos();
        }

        private async void ListaModelos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (ModelosListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarModelos(item.ModeloID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaModelos();
            }
        }

        private void BuscarModelos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarModelos.Text == "")
            {
                ListaModelos();
            }
            else
            {
                string Modelos = buscarModelos.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Modelos/ConsultarModelosPorModelos/{Modelos}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<ModelosListView>>(response.data.ToString());

                            listaModelos.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void AgregarNuevosModelos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Modelos.RegistrarModelos());
        }

        private async void ListaModelos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Modelos/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ModelosListView>>(response.data.ToString());

                    listaModelos.ItemsSource = listaView;


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