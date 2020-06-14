using Newtonsoft.Json;
using PCLAppConfig;
using RayTrackingMobile.Models;
using RTM.FormXamarin.Models;
using RTM.FormXamarin.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace RayTrackingMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login : ContentPage
    {
       // string connectionString = ConfigurationManager.AppSettings["KEY1"]


        public login()
        {
            InitializeComponent();

            btnIniciarSesion.Clicked += BtnIniciarSesion_Clicked;
        }

        private async void BtnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var usuario = txtUser.Text;
                var password = txtpassword.Text;

                if (string.IsNullOrEmpty(usuario))
                {
                    await DisplayAlert("Validacion", "Ingrese el nombre de Usuario", "Aceptar");
                    txtUser.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Validacion", "Ingrese el contrasena", "Aceptar");
                    txtUser.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var autenticarse = new IniciarSesion()
                {
                    NombreUsuario = usuario,
                    contrasena = password

                };

                var json = JsonConvert.SerializeObject(autenticarse);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Usuarios/IniciarSesionApi", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if ((bool)respuesta.data)
                    {
                        Application.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "Este usuario no existe en nuestra base de datos",
                                    title: "Login",
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
