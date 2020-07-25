using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Colores;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Forms.Internals;
using RTM.FormXamarin.ViewModels;

namespace RTM.FormXamarin.Views.Colores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarColores : ContentPage
    {
        RegistrarColoresViewModel RegistrarColoresViewModel;
        public RegistrarColores()
        {
            InitializeComponent();
            BindingContext = this.RegistrarColoresViewModel=new RegistrarColoresViewModel();
            btnGuardarColor.Clicked += BtnGuardarColor_Clicked;
        }

        private async void BtnGuardarColor_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreColorV = nombreColor.Text;


                if (string.IsNullOrEmpty(nombreColorV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Color", "Aceptar");
                    nombreColor.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var colores = new Colore()
                {
                    ColorID = 0,
                    Color = nombreColorV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(colores);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Colores/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Color registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Color no pudo registrarse correctamente",
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