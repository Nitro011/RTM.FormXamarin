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
using RTM.FormXamarin.Models.Suplidores;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Suplidores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarSuplidoresParaModificar : ContentPage
    {
        public ConsultarSuplidoresParaModificar()
        {
            InitializeComponent();
            ListaSuplidores();
            listaSuplidores.ItemSelected += ListaSuplidores_ItemSelected;
        }

        private void ListaSuplidores_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (SuplidoresListView)e.SelectedItem;

                Navigation.PushAsync(new ModificarSuplidores(item.SuplidorID));
            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async void ListaSuplidores()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Suplidores/SuplidoresList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<SuplidoresListView>>(response.data.ToString());

                    listaSuplidores.ItemsSource = listaView;


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