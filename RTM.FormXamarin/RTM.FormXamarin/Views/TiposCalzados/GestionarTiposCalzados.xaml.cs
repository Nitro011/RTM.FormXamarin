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