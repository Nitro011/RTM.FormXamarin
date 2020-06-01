using Newtonsoft.Json;
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
        public login()
        {
            InitializeComponent();
        }


        private async void btnLogin(object sender, EventArgs e)
        {



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
                    await DisplayAlert("Validacion", "Ingrese el password", "Aceptar");
                    txtUser.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://179.52.197.100:9090");

                var autenticarse = new Login()
                {
                    UserName = usuario,
                    PasswordHash = password

                };

                var json = JsonConvert.SerializeObject(autenticarse);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Usuarios/login", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
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
