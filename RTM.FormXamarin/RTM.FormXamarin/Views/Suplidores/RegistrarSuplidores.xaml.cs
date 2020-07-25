using Newtonsoft.Json;
using Org.Apache.Http.Impl;
using PCLAppConfig;
using RTM.FormXamarin.Models.Suplidores;
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
using RTM.FormXamarin.ViewModels;

namespace RTM.FormXamarin.Views.Suplidores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarSuplidores : ContentPage
    {
        RegistrarSuplidorViewModel RegistrarSuplidorViewModel;
        public RegistrarSuplidores()
        {
            InitializeComponent();
            BindingContext = this.RegistrarSuplidorViewModel = new RegistrarSuplidorViewModel();
            btnGuardarSuplidores.Clicked += BtnGuardarSuplidores_Clicked;
        }

        private async void BtnGuardarSuplidores_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreEmpresaV = nombreEmpresa.Text;
                var nombreSuplidorV = nombreSuplidor.Text;
                var telefonoV = telefono.Text;
                var correoElectronicoV = correoElectronico.Text;
                var paisV = pais.Text;
                var ciudadV = ciudad.Text;
                var direccionV = direccion.Text;

                if (string.IsNullOrEmpty(nombreEmpresaV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Nombre de la Empresa", "Aceptar");
                    nombreEmpresa.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(nombreSuplidorV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Nombre del Suplidor", "Aceptar");
                    nombreSuplidor.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(telefonoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el No de Telefono", "Aceptar");
                    telefono.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(correoElectronicoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Correo Electronico", "Aceptar");
                    correoElectronico.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(paisV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Pais", "Aceptar");
                    pais.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ciudadV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Ciudad", "Aceptar");
                    pais.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(direccionV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Direccion", "Aceptar");
                    direccion.Focus();
                    return;
                }

                //Con HttpClient se realiza la conexion a la base de datos:
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var suplidores = new Suplidore()
                {
                    SuplidorID = 0,
                    Empresa = nombreEmpresaV,
                    Nombre_Suplidor = nombreSuplidorV,
                    No_Telefono = telefonoV,
                    Correo_Electronico=correoElectronicoV,
                    Pais=paisV,
                    Ciudad=ciudadV,
                    Direccion = direccionV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(suplidores);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Suplidores/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Suplidor registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Suplidor no pudo registrarse correctamente",
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
            nombreEmpresa.Text = "";
            nombreSuplidor.Text = "";
            telefono.Text = "";
            correoElectronico.Text = "";
            pais.Text = "";
            ciudad.Text = "";
            direccion.Text = "";
        }
    }
}