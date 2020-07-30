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
using RTM.FormXamarin.Models.AreasDeProduccion;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.AreaDeProduccion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarAreaProduccion : ContentPage
    {
        public int areaProduccionID;
        public ModificarAreaProduccion(int AreaAreaProduccionID)
        {
            InitializeComponent();
            mostrarInformacionEmpleado(AreaAreaProduccionID);
            btnModificarAreaProduccion.Clicked += BtnModificarAreaProduccion_Clicked;
        }

        private async void BtnModificarAreaProduccion_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var areaProduccionIDV = areaProduccionID;
                var nombreAreaProduccionV = NombreAreaProduccion.Text;

                if (string.IsNullOrEmpty(nombreAreaProduccionV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el nombre del Departamento este ingresado", "Aceptar");
                    NombreAreaProduccion.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var areasProduccion = new AreaProduccion()
                {
                    AreaProduccionID = areaProduccionIDV,
                    NombreAreaProduccion =nombreAreaProduccionV,
                };

                var json = JsonConvert.SerializeObject(areasProduccion);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/AreaProduccion/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Departamento se modifico correctamente",
                                   title: "Registro",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Departamento no pudo modificarse correctamente",
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

        private void mostrarInformacionEmpleado(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/AreaProduccion/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<AreaProduccionListView>(response.data.ToString());

                    areaProduccionID = listaView.AreaProduccionID;
                    NombreAreaProduccion.Text = listaView.NombreAreaProduccion;
                }

            }
        }
    }
}