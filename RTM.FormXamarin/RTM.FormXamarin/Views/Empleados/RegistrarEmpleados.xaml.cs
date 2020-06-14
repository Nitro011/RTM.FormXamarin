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
        }

        private async void RegistroEmpleados(object sender, EventArgs e)
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
                var edadV = edad.Text;
                var fnV =  FN.Date;
                var rolV = roles.SelectedIndex;
                var passwordV = pass.Text;
                var userNameV = nUsuario.Text;
                var emailV = email.Text;
                


                if (string.IsNullOrEmpty(nombreV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de Usuario", "Aceptar");
                    nombre.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(apellidoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    apellido.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(direccionV))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    direccion.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(telefonoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    telefono.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cedulaV))
                {
                    await DisplayAlert("Validacion", "Ingrese la cedula", "Aceptar");
                    cedula.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(edadV))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    edad.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(fnV.ToString()))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    FN.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(passwordV))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    pass.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(userNameV))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    nUsuario.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(emailV))
                {
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    email.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var empleados = new Emple()
                {

                    Nombres = nombreV,
                    Apellidos = apellidoV,
                    Sexo = (sexo.SelectedIndex == 0) ? false : true,
                    Direccion = direccionV,
                    Telefono = telefonoV,
                    UserName = userNameV,
                    Fecha_Nacimiento = fnV,
                    Email = emailV,
                    RolID = rolV,
                    PasswordHash = passwordV,
                    Cedula = cedulaV

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(empleados);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Empleados/register", stringContent);

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