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
using RTM.FormXamarin.Models.TiposMateriales;

namespace RTM.FormXamarin.Views.TIposMateriales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarTiposMateriales : ContentPage
    {
        public RegistrarTiposMateriales()
        {
            InitializeComponent();
            btnGuardarTipoMaterial.Clicked += BtnGuardarTipoMaterial_Clicked;
        }

        private async void BtnGuardarTipoMaterial_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreTipoMaterialV = nombreTipoMaterial.Text;


                if (string.IsNullOrEmpty(nombreTipoMaterialV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Tipo de Material", "Aceptar");
                    nombreTipoMaterial.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var tipoMaterial = new TiposMateriales()
                {
                    Tipo_MaterialID = 0,
                    Nombre_Material = nombreTipoMaterialV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(tipoMaterial);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/TiposMateriales/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Tipo de Material registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Tipo de Material no pudo registrarse correctamente",
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