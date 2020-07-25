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
using RTM.FormXamarin.ViewModels;

namespace RTM.FormXamarin.Views.Dimensiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarDimensiones : ContentPage
    {
        RegistrarSizesViewModel RegistrarSizesViewModel;
        public RegistrarDimensiones()
        {
            InitializeComponent();
            BindingContext = this.RegistrarSizesViewModel=new RegistrarSizesViewModel();
            btnGuardarDimensiones.Clicked += BtnGuardarDimensiones_Clicked;
        }

        private async void BtnGuardarDimensiones_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var USAV = USA.Text;
                var UKV = UK.Text;
                var EUROV = EURO.Text;


                if (string.IsNullOrEmpty(USAV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Longitud", "Aceptar");
                    USA.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(UKV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Anchura", "Aceptar");
                    UK.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(EUROV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Altura", "Aceptar");
                    EURO.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var dimensiones = new Dimension()
                {
                    DimensionID = 0,
                    Longitud = Convert.ToInt32(USAV),
                    Anchura = Convert.ToInt32(UKV),
                    Altura=Convert.ToInt32(EUROV)

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