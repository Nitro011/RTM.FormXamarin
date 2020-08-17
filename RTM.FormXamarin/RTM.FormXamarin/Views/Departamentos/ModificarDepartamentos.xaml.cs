using Newtonsoft.Json;
using PCLAppConfig;
using RTM.FormXamarin.Models.Departamentos;
using RTM.FormXamarin.Models.TiposDepartamentos;
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

namespace RTM.FormXamarin.Views.Departamentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModificarDepartamentos : ContentPage
    {
        public int departamentoID;
        public ModificarDepartamentos(int DepartamentoID)
        {
            InitializeComponent();
            ListaTiposDepartamentos();
            mostrarInformacionDepartamentos(DepartamentoID);
            btnModificarDepartamento.Clicked += BtnModificarDepartamento_Clicked;
        }

        private async void BtnModificarDepartamento_Clicked(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];

            try
            {
                var DepartamentoIDV = departamentoID;
                var nombreDepartamentoV = nombreDepartamento.Text;
                var tipoDepartamentoIDV = (TiposDepartamentosListView)TiposDepartamentosComboBox.SelectedItem;

                if (string.IsNullOrEmpty(nombreDepartamentoV))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Departamento este ingresado", "Aceptar");
                    nombreDepartamento.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(tipoDepartamentoIDV.ToString()))
                {
                    await DisplayAlert("Validacion", "Asegurar que el Tipo de Departamento este seleccionado", "Aceptar");
                    return;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(connectionString);

                var departamentos = new Departamentoss()
                {
                    DepartamentoID = DepartamentoIDV,
                    Departamento=nombreDepartamentoV,
                    TipoDepartamentoID=tipoDepartamentoIDV.TipoDepartamentoID
                };

                var json = JsonConvert.SerializeObject(departamentos);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var request = await client.PostAsync("/api/Departamento/modificar", stringContent);

                if (request.IsSuccessStatusCode)
                {

                    var responseJson = await request.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Request>(responseJson);

                    if (respuesta.status)
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Departamento se modifico correctamente",
                                   title: "Modificacion",
                                   acknowledgementText: "Aceptar");
                    }
                    else
                    {
                        await MaterialDialog.Instance.AlertAsync(message: "El Departametno no pudo modificarse correctamente",
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
            await Navigation.PushAsync(new Departamentos.GestionarDepartamento());
        }

        private void mostrarInformacionDepartamentos(int DepartamentoID)
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync($"/api/Departamento/ObtenerDepartamentoPorID/{DepartamentoID}").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {
                    var listaView = JsonConvert.DeserializeObject<DepartamentosListView>(response.data.ToString());
                    departamentoID = listaView.DepartamentoID;
                    nombreDepartamento.Text = listaView.Departamento;
                    TiposDepartamentosComboBox.Text = listaView.TipoDepartamentoID.ToString();

                }

            }
        }

        private async void ListaTiposDepartamentos()
        {
            string connectionString = ConfigurationManager.AppSettings["ipServer"];


            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(connectionString);
            var request = client.GetAsync("/api/TiposDepartamentos/lista").Result;

            if (request.IsSuccessStatusCode)
            {
                var responseJson = request.Content.ReadAsStringAsync().Result;
                var response = JsonConvert.DeserializeObject<Request>(responseJson);

                if (response.status)
                {

                    var listaView = JsonConvert.DeserializeObject<List<TiposDepartamentosListView>>(response.data.ToString());

                    TiposDepartamentosComboBox.DataSource = listaView;


                }
                else
                {
                    await MaterialDialog.Instance.AlertAsync(message: "Error",
                                   title: "Error",
                                   acknowledgementText: "Aceptar");
                }
                await Navigation.PushAsync(new Departamentos.GestionarDepartamento());

            }

        }
    }
}