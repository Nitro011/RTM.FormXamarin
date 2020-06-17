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
using RTM.FormXamarin.Models.Usuarios;
using RTM.FormXamarin.Models.AreasDeProduccion;

namespace RTM.FormXamarin.Views.AreaDeProduccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarAreaDeProduccion : ContentPage
    {
        public RegistrarAreaDeProduccion()
        {
            InitializeComponent();
            btnGuardarAreaProduccion.Clicked += BtnGuardarAreaProduccion_Clicked;
        }

        private async void BtnGuardarAreaProduccion_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreAreaProduccionV = NombreAreaProduccion.Text;


                if (string.IsNullOrEmpty(nombreAreaProduccionV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de Area de Produccion", "Aceptar");
                    NombreAreaProduccion.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var empleados = new AreaProduccion()
                {
                    AreaProduccionID = 0,
                    NombreAreaProduccion = nombreAreaProduccionV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(empleados);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/AreaProduccion/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Area de Produccion registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Area de Produccion no pudo registrarse correctamente",
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