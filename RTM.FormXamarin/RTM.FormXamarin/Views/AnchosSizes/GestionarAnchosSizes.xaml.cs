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
using RTM.FormXamarin.Models.AnchosSizes;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.AnchosSizes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarAnchosSizes : ContentPage
    {
        public GestionarAnchosSizes()
        {
            InitializeComponent();
            agregarNuevosAnchosSizes.Clicked += AgregarNuevosAnchosSizes_Clicked;
            ListaAnchosSizes();
            listaAnchosSizes.ItemSelected += ListaAnchosSizes_ItemSelected;
            buscarAnchosSizes.TextChanged += BuscarAnchosSizes_TextChanged;
        }

        private void BuscarAnchosSizes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarAnchosSizes.Text == "")
            {
                ListaAnchosSizes();
            }
            else
            {
                string AnchosSizes = buscarAnchosSizes.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/AnchosSize/ConsultarAnchosSizesPorAnchoSizes/{AnchosSizes}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<AnchosSizesListView>>(response.data.ToString());

                            listaAnchosSizes.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void ListaAnchosSizes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (AnchosSizesListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarAnchosSizes(item.AnchoSizeID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaAnchosSizes();
            }
        }

        private async void AgregarNuevosAnchosSizes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AnchosSizes.RegistrarAnchosSizes());
        }

        private async void ListaAnchosSizes()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/AnchosSize/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<AnchosSizesListView>>(response.data.ToString());

                    listaAnchosSizes.ItemsSource = listaView;


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