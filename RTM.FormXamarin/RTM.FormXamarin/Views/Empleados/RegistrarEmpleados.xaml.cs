using Java.Sql;
using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.AreasDeProduccion;
using RTM.FormXamarin.Models.Roles;
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
            ListaAreaProduccion();
            ListaRoles();
        }

        private async void BtnGuardarEmpleados_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var rolIDV = pickerRoles.SelectedIndex+1;
                var areaProduccionIDV = pickerAreaProduccion.SelectedIndex+1;
                var codigoEmpleadoV = codigoEmpleado.Text;
                var nombreV = nombre.Text;
                var apellidoV = apellido.Text;
                var sexoV = sexo.SelectedIndex;
                var direccionV = direccion.Text;
                var telefonoV = telefono.Text;
                var cedulaV = cedula.Text;
                var fnV = FN.Date;
                var fechaIngresoV = FechaIngreso.Date;



                if (string.IsNullOrEmpty(rolIDV.ToString()))
                {
                    await DisplayAlert("Validacion", "Ingresar el puesto del empleado", "Aceptar");
                    pickerRoles.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(areaProduccionIDV.ToString()))
                {
                    await DisplayAlert("Validacion", "Ingresar el departamento del empleado", "Aceptar");
                    pickerAreaProduccion.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(codigoEmpleadoV))
                {
                    await DisplayAlert("Validacion", "Ingresar el codigo del empleado", "Aceptar");
                    codigoEmpleado.Focus();
                    return;
                }
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
                    await DisplayAlert("Validacion", "Ingresar la fecha de nacimiento del empleado", "Aceptar");
                    FN.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(fechaIngresoV.ToString()))
                {
                    await DisplayAlert("Validacion", "Ingresar la fecha de ingreso del empleado", "Aceptar");
                    FechaIngreso.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var empleados = new Emple()
                {
                    EmpleadoID = 0,
                    RolID = Convert.ToInt32(rolIDV),
                    AreaProduccionID=Convert.ToInt32(areaProduccionIDV),
                    CodigoEmpleado=codigoEmpleadoV,
                    Nombres = nombreV,
                    Apellidos = apellidoV,
                    Sexo = (sexo.SelectedIndex == 0) ? false : true,
                    Direccion = direccionV,
                    Telefono = telefonoV,
                    Fecha_Nacimiento = fnV,
                    Cedula = cedulaV,
                    Edad = DateTime.Now.Year - fnV.Value.Year,
                    FechaIngreso=fechaIngresoV

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

        private async void ListaAreaProduccion()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/AreaProduccion/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<AreaProduccionListView>>(response.data.ToString());

                    pickerAreaProduccion.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        private async void ListaRoles()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Role/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<RolesListView>>(response.data.ToString());

                    pickerRoles.ItemsSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

            }

        }

        async void ObtenerAreaProduccion(object sender, EventArgs e)
        {
            var pickerAreaProducciones = pickerAreaProduccion.SelectedIndex;

            await DisplayAlert("Mostrar Departamento", pickerAreaProducciones.ToString(), "Aceptar");
        }

        async void ObtenerRoles(object sender, EventArgs e)
        {
            var pickerRol = pickerRoles.SelectedIndex;

            await DisplayAlert("Mostrar Rol", pickerRol.ToString(), "Aceptar");
        }
    }
}