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
using RTM.FormXamarin.Models.TiposCalzados;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.TiposCalzados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarTiposCalzados : ContentPage
    {
        public int tipoCalzadoID;
        public ModificarTiposCalzados(int Tipo_CalzadoID)
        {
            InitializeComponent();
            mostrarInformacionTiposEstilos(Tipo_CalzadoID);
            btnModificarTipoCalzado.Clicked += BtnModificarTipoCalzado_Clicked;
        }

        private async void BtnModificarTipoCalzado_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var TipoCalzadoIDV = tipoCalzadoID;
                var TipoCalzadoV = nombreTipoCalzado.Text; ;

                if (string.IsNullOrEmpty(TipoCalzadoV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Tipo de Estilo este ingresado", "Aceptar");
                    nombreTipoCalzado.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var TiposCalzados = new TipoCalzado()
                {
                    Tipo_CalzadoID = TipoCalzadoIDV,
                    Tipo_Calzado=TipoCalzadoV
                };

                var json = JsonConvert.SerializeObject(TiposCalzados);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/TiposCalzados/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Tipo de Estilo se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Tipo de Estilo no pudo modificarse correctamente",
                                  title: "Modificacion",
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
            await Navigation.PushAsync(new TiposCalzados.GestionarTiposCalzados());
        }

        private void mostrarInformacionTiposEstilos(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/TiposCalzados/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<TiposCalzadosListView>(response.data.ToString());
                    tipoCalzadoID = listaView.Tipo_CalzadoID;
                    nombreTipoCalzado.Text = listaView.Tipo_Calzado;
                }

            }
        }
    }
}