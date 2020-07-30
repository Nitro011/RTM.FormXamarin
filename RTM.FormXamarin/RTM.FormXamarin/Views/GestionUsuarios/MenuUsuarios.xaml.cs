using Newtonsoft.Json;
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

namespace RTM.FormXamarin.Views.GestionUsuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuUsuarios : ContentPage
    {
        public MenuUsuarios()
        {
            InitializeComponent();
            ListaUsuarios();
            agregarNuevosUsuarios.Clicked += AgregarNuevosUsuarios_Clicked;
        }

        private async void AgregarNuevosUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GestionUsuarios.RegistrarUsuarios());
        }

        private async void ListaUsuarios()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Usuarios/UsuariosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<UsuariosListView>>(response.data.ToString());

                    listaUsuario.ItemsSource = listaView;


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