using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.OperacionesCalzados;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.OperacionesCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarOperacionesCalzados : ContentPage
    {
        public RegistrarOperacionesCalzados()
        {
            InitializeComponent();
            btnGuardarOperacionesCalzados.Clicked += BtnGuardarOperacionesCalzados_Clicked;
        }

        private async void BtnGuardarOperacionesCalzados_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var partNoV = PartNo.Text;
                var cantidadOperacionesV = cantidadOperaciones.Text;
                var descripcionV = descripcion.Text;
                var costoOperacionalV = costoOperacional.Text;

                if (string.IsNullOrEmpty(partNoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el PartNo", "Aceptar");
                    PartNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cantidadOperacionesV))
                {
                    await DisplayAlert("Validacion", "Ingrese las Cantidades de Operaciones", "Aceptar");
                    cantidadOperaciones.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(descripcionV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Descripcion", "Aceptar");
                    descripcion.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(costoOperacionalV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Costo Operacional", "Aceptar");
                    costoOperacional.Focus();
                    return;
                }

                //Con HttpClient se realiza la conexion a la base de datos:
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var operacionesCalzados = new OperacionesCalzado()
                {
                    OperacionesCalzadosID = 0,
                    PartNo = partNoV,
                    CantidadOperaciones = Convert.ToInt32(cantidadOperacionesV),
                    Descripcion = descripcionV,
                    CostoOperacional = Convert.ToDecimal(costoOperacionalV),
                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(operacionesCalzados);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/OperacionesCalzados/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Operacion de Calzado registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Operacion de Calzado no pudo registrarse correctamente",
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
            PartNo.Text = "";
            cantidadOperaciones.Text = "";
            descripcion.Text = "";
            costoOperacional.Text = "";
        }
    }
}