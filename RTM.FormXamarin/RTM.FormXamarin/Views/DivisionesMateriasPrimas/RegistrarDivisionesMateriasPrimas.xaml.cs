using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.DivisionesMateriasPrimas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.DivisionesMateriasPrimas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarDivisionesMateriasPrimas : ContentPage
    {
        public RegistrarDivisionesMateriasPrimas()
        {
            InitializeComponent();
            btnGuardarRegistrarDivisionMateriasPrimas.Clicked += BtnGuardarRegistrarDivisionMateriasPrimas_Clicked;
        }

        private async void BtnGuardarRegistrarDivisionMateriasPrimas_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreDivisionMateriasPrimasV = nombreDivisionesMateriasPrimas.Text;


                if (string.IsNullOrEmpty(nombreDivisionMateriasPrimasV))
                {
                    await DisplayAlert("Validacion", "Ingrese la división de la materia prima", "Aceptar");
                    nombreDivisionesMateriasPrimas.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var divisionMateriasPrimas = new DivisionesMateriasPrima()
                {
                    DivisionMateriaPrimaID = 0,
                    Division = nombreDivisionMateriasPrimasV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(divisionMateriasPrimas);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/DivisionesMateriasPrima/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La División de la materia prima se registro correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La División de la materia prima no pudo registrarse correctamente",
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
            await Navigation.PushAsync(new DivisionesMateriasPrimas.GestionarDivisionesMateriasPrimas());
        }

        private void limpiarCampos()
        {
            nombreDivisionesMateriasPrimas.Text = "";
        }
    }
}
