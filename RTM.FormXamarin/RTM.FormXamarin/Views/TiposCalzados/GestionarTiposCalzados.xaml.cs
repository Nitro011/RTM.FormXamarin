using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.TiposCalzados;
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

namespace RTM.FormXamarin.Views.TiposCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarTiposCalzados : ContentPage
    {
        TiposCalzadoViewModel TiposCalzadoViewModel;
        public GestionarTiposCalzados()
        {
            InitializeComponent();
            BindingContext = this.TiposCalzadoViewModel = new TiposCalzadoViewModel();
            ListaTiposCalzados();
            agregarNuevosTiposEstilos.Clicked += AgregarNuevosTiposEstilos_Clicked;
            buscarTiposEstilos.TextChanged += BuscarTiposEstilos_TextChanged;
            listaTiposCalzados.ItemSelected += ListaTiposCalzados_ItemSelected;
        }

        private async void ListaTiposCalzados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (TiposCalzadosListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarTiposCalzados(item.Tipo_CalzadoID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaTiposCalzados();
            }
        }

        private void BuscarTiposEstilos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarTiposEstilos.Text == "")
            {
                ListaTiposCalzados();
            }
            else
            {
                string TipoCalzado = buscarTiposEstilos.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/TiposCalzados/ConsultarTiposCalzadosPorTiposCalzados/{TipoCalzado}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<TiposCalzadosListView>>(response.data.ToString());

                            listaTiposCalzados.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void AgregarNuevosTiposEstilos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TiposCalzados.RegistrarTipoDeCalzado());
        }

        private async void ListaTiposCalzados()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/TiposCalzados/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<TiposCalzadosListView>>(response.data.ToString());

                    listaTiposCalzados.ItemsSource = listaView;


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