using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Modelos;
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

namespace RTM.FormXamarin.Views.Modelos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarModelos : ContentPage
    {
        public int modeloID;
        public ModificarModelos(int ModeloID)
        {
            InitializeComponent();
            mostrarInformacionModelos(ModeloID);
            btnModificarModelos.Clicked += BtnModificarModelos_Clicked;
        }

        private async void BtnModificarModelos_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var ModeloIDV = modeloID;
                var ModeloV = modelo.Text;

                if (string.IsNullOrEmpty(ModeloV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Modelo este ingresado", "Aceptar");
                    modelo.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var Modelos = new Modelo()
                {
                    ModeloID = ModeloIDV,
                    Modelo1 = ModeloV,
                };

                var json = JsonConvert.SerializeObject(Modelos);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Modelos/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Modelo no pudo modificarse correctamente",
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
            await Navigation.PushAsync(new Modelos.GestionarModelos());
        }

        private void mostrarInformacionModelos(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Modelos/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<ModelosListView>(response.data.ToString());
                    modeloID = listaView.ModeloID;
                    modelo.Text = listaView.Modelo1;
                }

            }
        }
    }
}