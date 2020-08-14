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
using RTM.FormXamarin.Models.ITEMS;

namespace RTM.FormXamarin.Views.ITEMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarItems : ContentPage
    {
        public GestionarItems()
        {
            InitializeComponent();
            ListaITEMS();
            listaItems.ItemSelected += ListaItems_ItemSelected;
            agregarNuevosItems.Clicked += AgregarNuevosItems_Clicked;
        }

        private async void ListaItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var ITEMS = (ITEMSListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarItems(ITEMS.ITEMID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaITEMS();
            }

        }

        private async void AgregarNuevosItems_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ITEMS.RegistrarItems());
        }

        private async void ListaITEMS()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/ITEM/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<ITEMSListView>>(response.data.ToString());

                    listaItems.ItemsSource = listaView;


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