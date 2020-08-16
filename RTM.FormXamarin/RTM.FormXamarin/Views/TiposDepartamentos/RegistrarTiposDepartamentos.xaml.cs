using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.TiposDepartamentos;
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

namespace RTM.FormXamarin.Views.TiposDepartamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarTiposDepartamentos : ContentPage
    {
        public RegistrarTiposDepartamentos()
        {
            InitializeComponent();
            btnGuardarTipoDepartamento.Clicked += BtnGuardarTipoDepartamento_Clicked;
        }

        private async void BtnGuardarTipoDepartamento_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var tipoDepartamentoV = tipoDepartamento.Text;


                if (string.IsNullOrEmpty(tipoDepartamentoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Tipo de Departamento", "Aceptar");
                    tipoDepartamento.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var colores = new TiposDepartamento()
                {
                    TipoDepartamentoID=0,
                    TipoDepartamento=tipoDepartamentoV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(colores);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/TiposDepartamentos/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Tipo Departamento registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Tipo Departamento no pudo registrarse correctamente",
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