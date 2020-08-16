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
using RTM.FormXamarin.Models.TiposDepartamentos;
using XF.Material.Forms.UI.Dialogs;

namespace RTM.FormXamarin.Views.TiposDepartamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarTiposDepartamentos : ContentPage
    {
        public int tipoDepartamentoID;
        public ModificarTiposDepartamentos(int TipoDepartamentoID)
        {
            InitializeComponent();
            mostrarInformacionTiposDepartamentos(TipoDepartamentoID);
            btnModificarTipoDepartamento.Clicked += BtnModificarTipoDepartamento_Clicked;
        }

        private async void BtnModificarTipoDepartamento_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var TipoDepartamentoIDV = tipoDepartamentoID;
                var TipoDepartamentoV = tipoDepartamento.Text;

                if (string.IsNullOrEmpty(TipoDepartamentoV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Tipo de Departamento este ingresado", "Aceptar");
                    tipoDepartamento.Focus();
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var Colores = new TiposDepartamento()
                {
                    TipoDepartamentoID=TipoDepartamentoIDV,
                    TipoDepartamento=TipoDepartamentoV
                };

                var json = JsonConvert.SerializeObject(Colores);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/TiposDepartamentos/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Tipo de Departamento se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Tipo de Departamento no pudo modificarse correctamente",
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
            await Navigation.PushAsync(new TiposDepartamentos.GestionarTiposDepartamentos());
        }

        private void mostrarInformacionTiposDepartamentos(int id)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/TiposDepartamentos/listaPorId/{id}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var listaView = JsonConvert.DeserializeObject<TiposDepartamentosListView>(response.data.ToString());
                    tipoDepartamentoID = listaView.TipoDepartamentoID;
                    tipoDepartamento.Text = listaView.TipoDepartamento;
                }

            }
        }
    }
}