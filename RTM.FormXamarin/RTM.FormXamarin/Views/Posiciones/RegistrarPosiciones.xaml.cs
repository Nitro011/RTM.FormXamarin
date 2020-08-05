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
    public partial class RegistrarPosiciones : ContentPage
    {
        public RegistrarPosiciones()
        {
            InitializeComponent();
            btnGuardarPosicion.Clicked += BtnGuardarPosicion_Clicked;
        }

        private async void BtnGuardarPosicion_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var posicionV = Posicion.Text;


                if (string.IsNullOrEmpty(posicionV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Nueva Posicion", "Aceptar");
                    Posicion.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var posiciones = new Posicione()
                {
                    PosicionID = 0,
                    Posicion = posicionV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(posiciones);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Posicion/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Posicion registrada correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Posicion no pudo registrarse correctamente",
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
                                    title: "Error",
                                    acknowledgementText: "Aceptar");
            }
            limpiarCampos();
            await Navigation.PushAsync(new Posiciones.GestionarPosiciones());
        }

        private void limpiarCampos()
        {
            Posicion.Text = "";
        }
    }
}