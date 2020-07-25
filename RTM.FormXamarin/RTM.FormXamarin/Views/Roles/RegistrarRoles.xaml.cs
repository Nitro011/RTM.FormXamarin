using PCLAppConfig;
using RTM.FormXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Models.Roles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Roles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarRoles : ContentPage
    {
        RegistrarRolesViewModel Rol;
        public RegistrarRoles()
        {
            InitializeComponent();
            BindingContext = this.Rol = new RegistrarRolesViewModel();
            btnGuardarPosicion.Clicked += BtnGuardarPosicion_Clicked;
        }

        private async void BtnGuardarPosicion_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var tipoUsuarioV = rol.Text;


                if (string.IsNullOrEmpty(tipoUsuarioV))
                {
                    await DisplayAlert("Validacion", "Ingrese la Nueva Posicion", "Aceptar");
                    rol.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var roles = new Role()
                {
                    RolID = 0,
                    Tipo_Usuario = tipoUsuarioV,

                };

                //Convetir a Json
                var json = JsonConvert.SerializeObject(roles);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                //Ejecutar el api el introduces el metodo
                var request = await client.PostAsync("/api/Role/registrar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    //Status
                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Posicion registrado correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Posicion no pudo registrarse correctamente",
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
            rol.Text = "";
        }
    }
}