using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.OperacionesCalzados;
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

namespace RTM.FormXamarin.Views.GestionProduccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoOperacionesCalzados : ContentPage
    {
        public ListadoOperacionesCalzados()
        {
            InitializeComponent();
            ListaOperacionesCalzados();
            //listaOperacionesCalzados.ItemSelected += ListaOperacionesCalzados_ItemSelected;
            buscarOperacionesEstilos.TextChanged += BuscarOperacionesEstilos_TextChanged;
        }

        //private async void ListaOperacionesCalzados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    bool answer = await DisplayAlert("Cread PDF?", "Desea crear un PDF de este elemento", "Si", "No");

        //    if (answer == true)
        //    {
        //        try
        //        {
        //            var item = (OperacionesCalzadosListView)e.SelectedItem;

        //            await Navigation.PushAsync(new GestionProduccion.);
        //        }
        //        catch (Exception ex)
        //        {

        //            await DisplayAlert("Error", ex.Message, "Aceptar");
        //        }
        //    }
        //    else
        //    {
        //        ListaOperacionesCalzados();
        //    }
        //}

        private  void BuscarOperacionesEstilos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarOperacionesEstilos.Text == "")
            {
                ListaOperacionesCalzados();
            }
            else
            {
                string PartNo = buscarOperacionesEstilos.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/OperacionesCalzados/ConsultarOperacionesCalzadosPorPartNo/{PartNo}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<OperacionesCalzadosListView>>(response.data.ToString());

                            /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                            listaOperacionesCalzados.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void ListaOperacionesCalzados()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/OperacionesCalzados/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<OperacionesCalzadosListView>>(response.data.ToString());

                    listaOperacionesCalzados.ItemsSource = listaView;


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