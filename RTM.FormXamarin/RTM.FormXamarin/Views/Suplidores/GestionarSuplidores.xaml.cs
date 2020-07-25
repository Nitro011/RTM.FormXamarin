using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Suplidores;
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

namespace RTM.FormXamarin.Views.Suplidores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarSuplidores : ContentPage
    {
        GestionarSuplidoresViewModel GestionarSuplidoresViewModel;
        public GestionarSuplidores()
        {
            InitializeComponent();
            BindingContext = this.GestionarSuplidoresViewModel = new GestionarSuplidoresViewModel();
            agregarNuevosSuplidores.Clicked += AgregarNuevosSuplidores_Clicked;
            ListaSuplidores();
            listaSuplidores.ItemSelected += ListaSuplidores_ItemSelected;
        }

        private void ListaSuplidores_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (SuplidoresListView)e.SelectedItem;

                Navigation.PushAsync(new InformacionDelSuplidor(item.Id));
            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async void AgregarNuevosSuplidores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Suplidores.RegistrarSuplidores());
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