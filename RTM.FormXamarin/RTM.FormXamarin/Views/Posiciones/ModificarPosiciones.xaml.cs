using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Posiciones;
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

namespace RTM.FormXamarin.Views.Posiciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarPosiciones : ContentPage
    {
        public int posicionID;
        public ModificarPosiciones(int PosicionID)
        {
            InitializeComponent();
            mostrarInformacionPosicion(PosicionID);
            btnModificarPosicion.Clicked += BtnModificarPosicion_Clicked;
        }

        private async void BtnModificarPosicion_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var posicionIDV = posicionID;
                var posicionV = Posicion.Text;

                if (string.IsNullOrEmpty(posicionV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el nombre de la Posicion este ingresado", "Aceptar");
                    Posicion.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var posiciones = new Posicione()
                {
                    PosicionID = posicionIDV,
                    Posicion = posicionV,
                };

                var json = JsonConvert.SerializeObject(posiciones);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Posicion/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Posicion se modifico correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Posicion no pudo modificarse correctamente",
                                  title: "Registro",
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
            await Navigation.PushAsync(new Posiciones.GestionarPosiciones());
        }

        private void mostrarInformacionPosicion(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Posicion/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<PosicionesListView>(response.data.ToString());

                    posicionID = listaView.PosicionID;
                    Posicion.Text = listaView.Posicion;
                }

            }
        }
        private void limpiarCampos()
        {
            Posicion.Text = "";
        }
    }
}