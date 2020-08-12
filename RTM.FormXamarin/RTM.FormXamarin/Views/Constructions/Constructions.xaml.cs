using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Constructions;
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

namespace RTM.FormXamarin.Views.Constructions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Constructions : ContentPage
    {
        public Constructions()
        {
            InitializeComponent();

            btnConstructions.Clicked += BtnConstructions_Clicked;
        }

        private async void BtnConstructions_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreConstructionsV = nombreConstructions.Text;


                if (string.IsNullOrEmpty(nombreConstructionsV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Constructions", "Aceptar");
                    nombreConstructions.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var constructions = new Construction()
                {
                    ConstructionID = 0,
                    Constructions = nombreConstructionsV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(constructions);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Constructions/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Modelo registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El modelo no pudo registrarse correctamente",
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
        }

        private void limpiarCampos()
        {
            nombreConstructions.Text = "";
        }
    }
}

