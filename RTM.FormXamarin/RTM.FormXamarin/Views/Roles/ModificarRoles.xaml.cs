using Newtonsoft.Json;
using PCLAppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RTM.FormXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RTM.FormXamarin.Models.Roles;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.Roles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarRoles : ContentPage
    {
        public int rolID;
        public ModificarRoles(int id)
        {
            InitializeComponent();
            mostrarInformacionPosicion(id);
            btnModificarPosicion.Clicked += BtnModificarPosicion_Clicked;
        }

        private async void BtnModificarPosicion_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var RolIDV = rolID;
                var TipoUsuarioV=rol.Text;

                if (string.IsNullOrEmpty(TipoUsuarioV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el nombre de la Posicion este ingresado", "Aceptar");
                    rol.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var roles = new Role()
                {
                    RolID=RolIDV,
                    Tipo_Usuario=TipoUsuarioV,
                };

                var json = JsonConvert.SerializeObject(roles);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Role/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Posicion se modifico correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "La Posicion no pudo modificarse correctamente",
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
            await Navigation.PushAsync(new GestionHumana.GestionHumana());
        }

        private void mostrarInformacionPosicion(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Role/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<RolesListView>(response.data.ToString());

                    rolID = listaView.RolID;
                    rol.Text = listaView.Tipo_Usuario;
                }

            }
        }
    }
}