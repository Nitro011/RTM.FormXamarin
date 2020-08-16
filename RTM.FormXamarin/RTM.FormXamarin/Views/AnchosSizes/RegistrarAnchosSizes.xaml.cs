using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.AnchosSizes;
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

namespace RTM.FormXamarin.Views.AnchosSizes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarAnchosSizes : ContentPage
    {
        public RegistrarAnchosSizes()
        {
            InitializeComponent();
            btnGuardarAnchosSizes.Clicked += BtnGuardarAnchosSizes_Clicked;
        }

        private async void BtnGuardarAnchosSizes_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreAnchosSizesV = nombreAnchosSizes.Text;


                if (string.IsNullOrEmpty(nombreAnchosSizesV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Ancho del Sizes", "Aceptar");
                    nombreAnchosSizes.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var colores = new AnchosSize()
                {
                    AnchoSizeID = 0,
                    AnchoSize = nombreAnchosSizesV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(colores);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/AnchosSize/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Ancho del Size se registro correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Ancho del Size no pudo registrarse correctamente",
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
            await Navigation.PushAsync(new AnchosSizes.GestionarAnchosSizes());
        }

        private void limpiarCampos()
        {
            nombreAnchosSizes.Text = "";
        }
    }
}