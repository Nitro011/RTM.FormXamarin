﻿using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Usuarios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;


namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarEmpleados : ContentPage
    {
        public ModificarEmpleados(int id)
        {
            InitializeComponent();
            mostrarInformacionEmpleado(id);

            EventModificar.Clicked += EventModificar_Clicked;      
        }

        private async void EventModificar_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var id = IdEmpleado.Text;
                var nombreV = Nombre.Text;
                var apellidoV = Apellido.Text;
                var sexoV = Sexo.SelectedIndex;
                var direccionV = Direccion.Text;
                var telefonoV = telefono.Text;
                var cedulaV = cedula.Text;
                var fnV = FN.Date;




                if (string.IsNullOrEmpty(nombreV))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de Usuario", "Aceptar");
                    Nombre.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(apellidoV))
                {
                    await DisplayAlert("Validacion", "Ingresar el apellido del empleado", "Aceptar");
                    Apellido.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(direccionV))
                {
                    await DisplayAlert("Validacion", "Ingreser la direccion del empleado", "Aceptar");
                    Direccion.Focus();
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
                    EmpleadoID =int.Parse(id),
                    Nombres = nombreV,
                    Apellidos = apellidoV,
                    Sexo = (Sexo.SelectedIndex == 0) ? false : true,
                    Direccion = direccionV,
                    Telefono = telefonoV,
                    Fecha_Nacimiento = fnV,
                    Cedula = cedulaV,
                    Edad =DateTime.Now.Year - fnV.Value.Year

                };

                var json = JsonConvert.SerializeObject(empleados);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Empleados/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Usuario se modifico correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Usuario no pudo modificarse correctamente",
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
                                    title: ex.Message,
                                    acknowledgementText: "Aceptar");
            }


        }

        private void mostrarInformacionEmpleado(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Empleados/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<Emple>(response.data.ToString());

                    var año = (listaView.Fecha_Nacimiento != null) ? listaView.Fecha_Nacimiento.Value.Year : DateTime.MinValue.Year;
                    IdEmpleado.Text = listaView.EmpleadoID.ToString();
                    Nombre.Text = listaView.Nombres;
                    Apellido.Text = listaView.Apellidos;
                    Sexo.SelectedIndex = (listaView.Sexo == true) ? 1 : 0;
                    cedula.Text = listaView.Cedula;
                    FN.Date = listaView.Fecha_Nacimiento;
                    Direccion.Text = listaView.Direccion;
                    telefono.Text = listaView.Telefono;
                }

            }
        }



    }
}