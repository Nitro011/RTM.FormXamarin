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
using XF.Material.Forms.UI.Dialogs;
using RTM.FormXamarin.Models.DivisionesMateriasPrimas;


namespace RTM.FormXamarin.Views.DivisionesMateriasPrimas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDivisionesMateriasPrimas : ContentPage
    {

        public GestionarDivisionesMateriasPrimas()
        {
            InitializeComponent();
            listaDivisionMateriaPrima.ItemSelected += ListaDivisionMateriaPrima_ItemSelected;
            agregarNuevaDivisionMateriaPrima.Clicked += AgregarNuevaDivisionMateriaPrima_Clicked;
            buscarDivisionMateriaPrima.TextChanged += BuscarDivisionMateriaPrima_TextChanged1;
            ListaDivisionesMateriasPrimas();
        }

        private void BuscarDivisionMateriaPrima_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if (buscarDivisionMateriaPrima.Text == "")
            {
                ListaDivisionesMateriasPrimas();
            }
            else
            {
                string Divisiones = buscarDivisionMateriaPrima.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/DivisionesMateriasPrima/ConsultarDivisionesMateriasPrimasPorDivisiones/{Divisiones}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<DivisionesMateriasPrimasListView>>(response.data.ToString());

                            listaDivisionMateriaPrima.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void ListaDivisionMateriaPrima_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (DivisionesMateriasPrimasListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarDivisionesMateriasPrimas(item.DivisionMateriaPrimaID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaDivisionesMateriasPrimas();
            }

        }

        private async void ListaDivisionesMateriasPrimas()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/DivisionesMateriasPrima/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<DivisionesMateriasPrimasListView>>(response.data.ToString());

                    listaDivisionMateriaPrima.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }
        }
        private async void AgregarNuevaDivisionMateriaPrima_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DivisionesMateriasPrimas.RegistrarDivisionesMateriasPrimas());  
        }

        private async void AgregarNuevosColores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Colores.RegistrarColores());
        }

    }
}