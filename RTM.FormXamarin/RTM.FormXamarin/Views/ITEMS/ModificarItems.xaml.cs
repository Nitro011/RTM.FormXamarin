using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.ITEMS;
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

namespace RTM.FormXamarin.Views.ITEMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarItems : ContentPage
    {
        public int ItemsID;
        public ModificarItems(int id)
        {
            InitializeComponent();
            mostrarInformacionItem(id);
            btnModificarItem.Clicked += BtnModificarItem_Clicked;
        }

        private async void BtnModificarItem_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var ItemIDV = ItemsID;
                var ItemV = nombreItem.Text;

                if (string.IsNullOrEmpty(ItemV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Item este ingresado", "Aceptar");
                    nombreItem.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var Item = new ITEM()
                {
                    ItemID = ItemIDV,
                    item = ItemV
                };

                var json = JsonConvert.SerializeObject(Item);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Item/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Item se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Item no pudo modificarse correctamente",
                                  title: "Modificacion",
                                  acknowledgementText: "Aceptar");

                    }

                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                    title: "Error",
                                    acknowledgementText: "Aceptar");
                }

            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.AlertAsync(message: ex.Message,
                                    title: ex.Message,
                                    acknowledgementText: "Aceptar");
            }
            await Navigation.PushAsync(new Colores.GestionarColores());
        }

        private void mostrarInformacionItem(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Item/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var listaView = JsonConvert.DeserializeObject<ITEMSListView>(response.data.ToString());
                     ItemsID= listaView.ItemID;
                     nombreItem.Text = listaView.item;
                }

            }
        }
    }
}