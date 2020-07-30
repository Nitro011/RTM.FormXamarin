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
using RTM.FormXamarin.Models.Marcas;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Marcas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarMarcas : ContentPage
    {
        public int marcaID;
        public ModificarMarcas(int MarcaID)
        {
            InitializeComponent();
            mostrarInformacionMarcas(MarcaID);
            btnModificarMarcas.Clicked += BtnModificarMarcas_Clicked;
        }

        private async void BtnModificarMarcas_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var MarcasIDV = marcaID;
                var MarcasV = marcas.Text;

                if (string.IsNullOrEmpty(MarcasV))
                {
                    await DisplayAlert("Validacion", "Asegurar que la Marca este ingresada", "Aceptar");
                    marcas.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var Marcas = new Marca()
                {
                    MarcaID = MarcasIDV,
                    Marca1=MarcasV,
                };

                var json = JsonConvert.SerializeObject(Marcas);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Marcas/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Marca se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Marca no pudo modificarse correctamente",
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
            await Navigation.PushAsync(new Marcas.GestionarMarcas());
        }

        private void mostrarInformacionMarcas(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Marcas/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<MarcasListView>(response.data.ToString());

                    marcaID = listaView.MarcaID;
                    marcas.Text = listaView.Marca1;

                }

            }
        }
    }
}