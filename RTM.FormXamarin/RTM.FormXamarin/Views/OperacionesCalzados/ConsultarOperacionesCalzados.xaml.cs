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
using RTM.FormXamarin.Models.OperacionesCalzados;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.OperacionesCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarOperacionesCalzados : ContentPage
    {
        public ConsultarOperacionesCalzados()
        {
            InitializeComponent();
            ListaOperacionesCalzados();
            listaOperacionesCalzados.ItemSelected += ListaOperacionesCalzados_ItemSelected;
        }

        private void ListaOperacionesCalzados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (OperacionesCalzadosListView)e.SelectedItem;

                Navigation.PushAsync(new InformacionOperacionesCalzados(item.OperacionesCalzadosID));
            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Aceptar");
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