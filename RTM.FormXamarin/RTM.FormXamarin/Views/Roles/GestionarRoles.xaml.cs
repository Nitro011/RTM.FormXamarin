using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Roles;
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

namespace RTM.FormXamarin.Views.Roles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionarRoles : ContentPage
    {
        public GestionarRoles()
        {
            InitializeComponent();
            ListaRoles();
            buscarRoles.TextChanged += BuscarRoles_TextChanged;
            agregarNuevoRol.Clicked += AgregarNuevoRol_Clicked;
        }

        private async void AgregarNuevoRol_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Roles.RegistrarRoles());
        }

        private void BuscarRoles_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarRoles.Text == "")
            {
                ListaRoles();
            }
            else
            {
                string TipoUsuario = buscarRoles.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Role/BuscarRolesPorTipoUsuario/{TipoUsuario}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {

                        var listaView = JsonConvert.DeserializeObject<List<RolesListView>>(response.data.ToString());

                        /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                        listaPosiciones.ItemsSource = listaView;
                    }

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

                    listaPosiciones.ItemsSource = listaView;


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