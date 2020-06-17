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

namespace RTM.FormXamarin.Views.Empleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarEmpleadosParaModificar : ContentPage
    {
        public ConsultarEmpleadosParaModificar()
        {
            InitializeComponent();
            ListaEmpleado();
            listaEmpleado.ItemSelected += ListaEmpleado_ItemSelected;
        }

        private void ListaEmpleado_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (EmpleadoListView)e.SelectedItem;

                Navigation.PushAsync(new ModificarEmpleados(item.Id));
            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Aceptar");
            }

        }



        private void ListaEmpleado()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/Empleados/EmpleadosList").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<EmpleadoListView>>(response.data.ToString());

                    listaEmpleado.ItemsSource = listaView;


                }

            }

        }
    }
}