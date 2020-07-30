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
using RTM.FormXamarin.Models.Usuarios;
using RTM.FormXamarin.Views.Empleados;
using RTM.FormXamarin.ViewModels;
using RTM.FormXamarin.Models.AreasDeProduccion;
using RTM.FormXamarin.Views.AreaDeProduccion;
using RTM.FormXamarin.Views.Roles;

namespace RTM.FormXamarin.Views.GestionHumana
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionHumana : TabbedPage
    {
        GestionHumanaViewModels GestionHumanaViewModels;
        public GestionHumana()
        {
            InitializeComponent();
            BindingContext = this.GestionHumanaViewModels = new GestionHumanaViewModels();
            ListaPosiciones();
            ListaEmpleado();
            ListaAreaProduccion();
            listaPosiciones.ItemSelected += ListaPosiciones_ItemSelected;
            listaAreaProduccion.ItemSelected += ListaAreaProduccion_ItemSelected;
            buscarPosiciones.TextChanged += BuscarPosiciones_TextChanged;
            listaEmpleado.ItemSelected += ListaEmpleado_ItemSelected;
            buscarEmpleado.TextChanged += BuscarEmpleado_TextChanged;
            buscarDepartamento.TextChanged += BuscarDepartamento_TextChanged;
            agregarNuevoEmpleado.Clicked += AgregarNuevoEmpleado_Clicked;
            agregarNuevaPosicion.Clicked += AgregarNuevaPosicion_Clicked;
            agregarDepartamentos.Clicked += AgregarDepartamentos_Clicked;
        }

        private async void ListaPosiciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (RolesListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarRoles(item.RolID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaPosiciones();
            }
        }

        private async void ListaAreaProduccion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Modificar?", "Desea modificar este elemento", "Si", "No");

            if (answer == true)
            {
                try
                {
                    var item = (AreaProduccionListView)e.SelectedItem;

                    await Navigation.PushAsync(new ModificarAreaProduccion(item.AreaProduccionID));
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", ex.Message, "Aceptar");
                }
            }
            else
            {
                ListaAreaProduccion();
            }
        }

        private void BuscarDepartamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarDepartamento.Text == "")
            {
                ListaAreaProduccion();
            }
            else
            {
                string NombreAreaProduccion = buscarDepartamento.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/AreaProduccion/BuscarAreaProduccionPorNombre/{NombreAreaProduccion}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<AreaProduccionListView>>(response.data.ToString());

                            /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                            listaAreaProduccion.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private async void AgregarDepartamentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AreaDeProduccion.RegistrarAreaDeProduccion());
        }

        private async void AgregarNuevaPosicion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Roles.RegistrarRoles());
        }

        private async void AgregarNuevoEmpleado_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados.RegistrarEmpleados());
        }

        private void BuscarEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarEmpleado.Text == "")
            {
                ListaEmpleado();
            }
            else
            {
                string Nombres = buscarEmpleado.Text;
                string Apellidos = buscarEmpleado.Text;
                string Cedula = buscarEmpleado.Text;
                string CodigoEmpleado = buscarEmpleado.Text;

                string connectionString = ConfigurationManager.AppSettings["ipServer"];


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(connectionString);
                var request = client.GetAsync($"/api/Empleados/EmpleadoPorNombresApellidosCedulaCodigoEmpleado/{Nombres}/{Apellidos}/{Cedula}/{CodigoEmpleado}").Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseJson = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (response.status)
                    {
                        if (response.data != null)
                        {

                            var listaView = JsonConvert.DeserializeObject<List<EmpleadoListView>>(response.data.ToString());

                            /*  var año = (listaView.fecha_nacimiento != null) ? listaView.fecha_nacimiento.Value.Year : DateTime.MinValue.Year;*/
                            listaEmpleado.ItemsSource = listaView;
                        }
                    }

                }
            }
        }

        private void ListaEmpleado_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = (EmpleadoListView)e.SelectedItem;

                Navigation.PushAsync(new InformacionDelEmpleado(item.Id));
            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private void BuscarPosiciones_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buscarPosiciones.Text == "")
            {
                ListaPosiciones();
            }
            else
            {
                string TipoUsuario = buscarPosiciones.Text;

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

        //Funcionalidad para el tab de posiciones:
        //Este codigo trae todo el listado de cada una de las posiciones (Roles) desde la base de datos:
        private async void ListaPosiciones()
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

        private async void ListaEmpleado()
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
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }

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

                    listaAreaProduccion.ItemsSource = listaView;


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