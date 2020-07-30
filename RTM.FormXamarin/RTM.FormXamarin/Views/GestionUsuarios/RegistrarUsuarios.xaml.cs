﻿using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Usuarios;
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
using RTM.FormXamarin.Models.Empleados;
using RTM.FormXamarin.Models.AreasDeProduccion;

namespace RTM.FormXamarin.Views.GestionUsuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarUsuarios : ContentPage
    {
        public RegistrarUsuarios()
        {
            InitializeComponent();
            btnGuardarUsuarios.Clicked += BtnGuardarUsuarios_Clicked;
            ListaAreaProduccion();
            searchEmpleado.TextChanged += SearchEmpleado_TextChanged;

        }

        private void SearchEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            string SearchEmpleado = searchEmpleado.Text;

            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Empleados/EmpleadoPorCedula/{SearchEmpleado}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    if (response.data != null)
                    {
                        var listaView = JsonConvert.DeserializeObject<EmpleadoPorCedula>(response.data.ToString());


                        /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                        empleadoID.Text = listaView.EmpleadoID.ToString();
                        nombreEmpleado.Text = listaView.nombreCompleto;
                    }

                }
                else
                {
                    MaterialDialog.Instance.AlertAsync(message: "Error",
                                                       title: "Error",
                                                       acknowledgementText: "Aceptar");
                }

            }
        }

        private async void BtnGuardarUsuarios_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var NombreDeUsuarioV = nombreUsuario.Text;
                var ContrasenaV = passwordHash.Text;
                var CorreoElectronicoV = email.Text;
                var EmpleadoIDV = empleadoID.Text;
                var NombreEmpleadV = nombreEmpleado.Text;
                var RolIDV = pickerRoles.SelectedIndex;
                var AreaProduccionIDV = pickerAreaProduccion.SelectedIndex + 1;


                if (string.IsNullOrEmpty(NombreDeUsuarioV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Nombre de Usuario", "Aceptar");
                    nombreUsuario.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ContrasenaV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Contraseña", "Aceptar");
                    passwordHash.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(CorreoElectronicoV))
                {
                    await DisplayAlert("Validacion", "Ingrese el Correo Electronico", "Aceptar");
                    email.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(NombreEmpleadV))
                {
                    await DisplayAlert("Validacion", "Verifique el Nombre del Empleado", "Aceptar");
                    nombreEmpleado.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var usuario = new Usuario()
                {
                    UsuarioID = 0,
                    NombreDeUsuario = NombreDeUsuarioV,
                    Contrasena = ContrasenaV,
                    CorreoElectronico = CorreoElectronicoV,
                    EmpleadoID = Convert.ToInt32(EmpleadoIDV),
                    RolID = Convert.ToInt32(RolIDV),
                    AreaProduccionID = Convert.ToInt32(AreaProduccionIDV)
                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(usuario);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Usuarios/registrar", stringContent);

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
    }
}