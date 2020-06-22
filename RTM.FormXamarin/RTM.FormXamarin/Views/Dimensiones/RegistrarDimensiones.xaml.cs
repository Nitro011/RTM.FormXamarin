using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Dimensiones;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Dimensiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarDimensiones : ContentPage
    {
        public RegistrarDimensiones()
        {
            InitializeComponent();
            btnGuardarDimensiones.Clicked += BtnGuardarDimensiones_Clicked;
        }

        private async void BtnGuardarDimensiones_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var longitudV = longitud.Text;
                var anchuraV = anchura.Text;
                var alturaV = altura.Text;


                if (string.IsNullOrEmpty(longitudV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Longitud", "Aceptar");
                    longitud.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(anchuraV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Anchura", "Aceptar");
                    anchura.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(alturaV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Altura", "Aceptar");
                    altura.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var dimensiones = new Dimension()
                {
                    DimensionID = 0,
                    Longitud = Convert.ToInt32(longitudV),
                    Anchura = Convert.ToInt32(anchuraV),
                    Altura=Convert.ToInt32(alturaV)

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(dimensiones);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Dimensiones/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Dimensiones registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Dimensiones no pudo registrarse correctamente",
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
        }
    }
}