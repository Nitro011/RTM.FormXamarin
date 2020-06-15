using Java.Sql;
using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Usuarios;

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

namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarEmpleados : ContentPage
    {
        EmpleadosViewModel Empleados;


        public RegistrarEmpleados()
        {
            InitializeComponent();
            BindingContext = this.Empleados = new EmpleadosViewModel();
            btnGuardarEmpleados.Clicked += BtnGuardarEmpleados_Clicked;
        }

        private async void BtnGuardarEmpleados_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var nombreV = nombre.Text;
                var apellidoV = apellido.Text;
                var sexoV = sexo.SelectedIndex;
                var direccionV = direccion.Text;
                var telefonoV = telefono.Text;
                var cedulaV = cedula.Text;
                var fnV = FN.Date;





                if (string.IsNullOrEmpty(nombreV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de Usuario", "Aceptar");
                    nombre.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(apellidoV))
                {
                    await DisplayAlert("Validacion", "Ingresar el apellido del empleado", "Aceptar");
                    apellido.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(direccionV))
                {
                    await DisplayAlert("Validacion", "Ingreser la direccion del empleado", "Aceptar");
                    direccion.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(telefonoV))
                {
                    await DisplayAlert("Validacion", "Ingresar el numero telefonico del empleado", "Aceptar");
                    telefono.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cedulaV))
                {
                    await DisplayAlert("Validacion", "Ingreser la cedula del empleado", "Aceptar");
                    cedula.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(fnV.ToString()))
                {
                    await DisplayAlert("Validacion", "Ingresar la fecha de nacimiento del usuario", "Aceptar");
                    FN.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var empleados = new Emple()
                {
                    EmpleadoId = 0,
                    Nombres = nombreV,
                    Apellidos = apellidoV,
                    Sexo = (sexo.SelectedIndex == 0) ? false : true,
                    Direccion = direccionV,
                    Telefono = telefonoV,
                    Fecha_Nacimiento = fnV,
                    Cedula = cedulaV,
                    Edad = DateTime.Now.Year - fnV.Value.Year

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(empleados);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Empleados/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Usuario registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Usuario no pudo registrarse correctamente",
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