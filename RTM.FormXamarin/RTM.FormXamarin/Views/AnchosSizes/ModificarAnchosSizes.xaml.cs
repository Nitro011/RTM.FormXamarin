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
using RTM.FormXamarin.Models.AnchosSizes;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.AnchosSizes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarAnchosSizes : ContentPage
    {
        public int anchoSizeID;
        public ModificarAnchosSizes(int AnchoSizeID)
        {
            InitializeComponent();
            mostrarInformacionAnchosSizes(AnchoSizeID);
            btnModificarAnchosSizes.Clicked += BtnModificarAnchosSizes_Clicked;
        }

        private async void BtnModificarAnchosSizes_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var AnchoSizeIDV = anchoSizeID;
                var nombreAnchosSizesV = nombreAnchosSizes.Text;

                if (string.IsNullOrEmpty(nombreAnchosSizesV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Ancho del Size", "Aceptar");
                    nombreAnchosSizes.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var AnchoSizes = new AnchosSize()
                {
                    AnchoSizeID = AnchoSizeIDV,
                    AnchoSize = nombreAnchosSizesV
                };

                var json = JsonConvert.SerializeObject(AnchoSizes);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/AnchosSize/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Ancho del Size se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Ancho del Size no pudo modificarse correctamente",
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
            limpiarCampos();
            await Navigation.PushAsync(new AnchosSizes.GestionarAnchosSizes());
        }

        private void mostrarInformacionAnchosSizes(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/AnchosSize/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var listaView = JsonConvert.DeserializeObject<AnchosSizesListView>(response.data.ToString());
                    anchoSizeID = listaView.AnchoSizeID;
                    nombreAnchosSizes.Text = listaView.AnchoSize;
                }

            }
        }

        private void limpiarCampos()
        {
            nombreAnchosSizes.Text = "";
        }
    }
}