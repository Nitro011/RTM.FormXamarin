using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Clientes;
using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarClientes : ContentPage
    {
        ClientesViewModel Clientes;

        public RegistrarClientes()
        {
            InitializeComponent();
            BindingContext = this.Clientes = new ClientesViewModel();
            btnGuardarClientes.Clicked += BtnGuardarClientes_Clicked;


        }

        private async void BtnGuardarClientes_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var codigoClienteV = codigoCliente.Text;
                var RNCV = RNC.Text;
                var nombreClienteV = nombreCliente.Text;
                var correoElectronicoV = correoElectronico.Text;
                var noTelefonoV = noTelefono.Text;
                var paisV = pais.Text;
                var direccionV = direccion.Text;

                if (string.IsNullOrEmpty(codigoClienteV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Codigo de Cliente", "Aceptar");
                    codigoCliente.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(RNCV))
                {
                    await DisplayAlert("Validacion", "Ingrese el RNC del Cliente", "Aceptar");
                    RNC.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(nombreClienteV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre del Cliente", "Aceptar");
                    nombreCliente.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(correoElectronicoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el correo electronico del Cliente", "Aceptar");
                    correoElectronico.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(noTelefonoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el numero de telefono del Cliente", "Aceptar");
                    noTelefono.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(paisV))
                {
                    await DisplayAlert("Validacion","Ingrese el pais del Cliente","Aceptar");
                    pais.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var clientes = new Cliente()
                {
                    ClienteID = 0,
                    CodigoCliente = codigoClienteV,
                    RNC=RNCV,
                    Nombre_Cliente=nombreClienteV,
                    Correo_Electronico=correoElectronicoV,
                    No_Telefono=noTelefonoV,
                    Pais=paisV,
                    Direccion=direccionV
                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(clientes);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Clientes/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Cliente registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Cliente no pudo registrarse correctamente",
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
            await Navigation.PushAsync(new Clientes.GestionarClientes());
        }

        private void limpiarCampos()
        {
            codigoCliente.Text="";
            RNC.Text = "";
            nombreCliente.Text = "";
            correoElectronico.Text = "";
            noTelefono.Text = "";
            pais.Text = "";
            direccion.Text = "";
        }
    }
}