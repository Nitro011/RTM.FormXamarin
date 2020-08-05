using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Posiciones;
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
using Rg.Plugins.Popup.Extensions;

namespace RTM.FormXamarin.Views.Posiciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarPosiciones : ContentPage
    {
        public GestionarPosiciones()
        {
            InitializeComponent();
            ListaPosiciones();
            listaPosiciones.ItemSelected += ListaPosiciones_ItemSelected;
            buscarPosiciones.TextChanged += BuscarPosiciones_TextChanged;
            agregarNuevaPosicion.Clicked += AgregarNuevaPosicion_Clicked;
        }

        private async void ListaPosiciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (PosicionesListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarPosiciones(item.PosicionID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaPosiciones();
            }
        }

        private async void AgregarNuevaPosicion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Posiciones.RegistrarPosiciones());
        }

        private void BuscarPosiciones_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarPosiciones.Text == "")
            {
                ListaPosiciones();
            }
            else
            {
                string Posicion = buscarPosiciones.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Posicion/ConsultarPosicionesPorPosicion/{Posicion}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {

                        var listaView = JsonConvert.DeserializeObject<List<PosicionesListView>>(response.data.ToString());

                        /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                        listaPosiciones.ItemsSource = listaView;
                    }

                }
            }
        }

        private async void ListaPosiciones()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Posicion/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<PosicionesListView>>(response.data.ToString());

                    listaPosiciones.ItemsSource = listaView;


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