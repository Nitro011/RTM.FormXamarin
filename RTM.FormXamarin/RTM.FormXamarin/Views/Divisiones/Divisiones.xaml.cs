using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Divisiones;
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

namespace RTM.FormXamarin.Views.Divisiones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Divisiones : ContentPage
    {
        public Divisiones()
        {
            InitializeComponent();
            btnDivision.Clicked += BtnDivision_Clicked;
        }

        private async void BtnDivision_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreDivisionV = nombreDivision.Text;


                if (string.IsNullOrEmpty(nombreDivisionV))
                {
                    await DisplayAlert("Validacion", "Ingrese la división que pertenece", "Aceptar");
                    nombreDivision.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var division = new Divisione()
                {
                    DivisionID = 0,
                    Division = nombreDivisionV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(division);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Divisiones/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "División registrada correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La divión no pudo registrarse correctamente",
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