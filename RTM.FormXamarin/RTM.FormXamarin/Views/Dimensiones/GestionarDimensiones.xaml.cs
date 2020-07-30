using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Sizes;
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

namespace RTM.FormXamarin.Views.Dimensiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarDimensiones : ContentPage
    {
        GestionarDimensionesViewModel GestionarDimensionesViewModel;
        public GestionarDimensiones()
        {
            InitializeComponent();
            BindingContext = this.GestionarDimensionesViewModel = new GestionarDimensionesViewModel();
            ListaSizes();
            agregarNuevosSizes.Clicked += AgregarNuevosSizes_Clicked;
        }

        private async void AgregarNuevosSizes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Dimensiones.RegistrarDimensiones());
        }

        private async void ListaSizes()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Size/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<SizesListView>>(response.data.ToString());

                    listaSizes.ItemsSource = listaView;


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