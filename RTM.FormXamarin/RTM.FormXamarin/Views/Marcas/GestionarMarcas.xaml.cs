using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Marcas;
using RTM.FormXamarin.ViewModels;
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

namespace RTM.FormXamarin.Views.Marcas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarMarcas : ContentPage
    {
        GestionarMarcasViewModel GestionarMarcasViewModel = new GestionarMarcasViewModel();
        public GestionarMarcas()
        {
            InitializeComponent();
            BindingContext = this.GestionarMarcasViewModel = new GestionarMarcasViewModel();
            ListaMarcas();
            agregarNuevaMarcas.Clicked += AgregarNuevaMarcas_Clicked;
            buscarMarcas.TextChanged += BuscarMarcas_TextChanged;
            listaMarcas.ItemSelected += ListaMarcas_ItemSelected;
        }

        private async void ListaMarcas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (MarcasListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarMarcas(item.MarcaID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaMarcas();
            }
        }

        private void BuscarMarcas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarMarcas.Text == "")
            {
                ListaMarcas();
            }
            else
            {
                string Marca = buscarMarcas.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Marcas/ConsultarMarcasPorMarca/{Marca}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<MarcasListView>>(response.data.ToString());

                            listaMarcas.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void AgregarNuevaMarcas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Marcas.RegistrarMarcas());
        }

        private async void ListaMarcas()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Marcas/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<MarcasListView>>(response.data.ToString());

                    listaMarcas.ItemsSource = listaView;


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